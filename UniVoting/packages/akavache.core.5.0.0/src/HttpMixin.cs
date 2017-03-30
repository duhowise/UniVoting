﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Splat;
using System.Collections.Concurrent;

#if SILVERLIGHT
using Akavache.Internal;
#endif

#if SILVERLIGHT
using System.Net.Browser;
#endif

namespace Akavache
{

    public class AkavacheHttpMixin : IAkavacheHttpMixin
    {
#if SILVERLIGHT
        static AkavacheHttpMixin()
        {
            WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);
        }
#endif

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. The URL itself is used as the key.
        /// </summary>
        /// <param name="url">The URL to download.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public IObservable<byte[]> DownloadUrl(IBlobCache This, string url, IDictionary<string, string> headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null)
        {
            return This.DownloadUrl(url, url, headers, fetchAlways, absoluteExpiration);
        }

        /// <summary>
        /// Download data from an HTTP URL and insert the result into the
        /// cache. If the data is already in the cache, this returns
        /// a cached value. An explicit key is provided rather than the URL itself.
        /// </summary>
        /// <param name="key">The key to store with.</param>
        /// <param name="url">The URL to download.</param>
        /// <param name="headers">An optional Dictionary containing the HTTP
        /// request headers.</param>
        /// <param name="fetchAlways">Force a web request to always be issued, skipping the cache.</param>
        /// <param name="absoluteExpiration">An optional expiration date.</param>
        /// <returns>The data downloaded from the URL.</returns>
        public IObservable<byte[]> DownloadUrl(IBlobCache This, string key, string url, IDictionary<string, string> headers = null, bool fetchAlways = false, DateTimeOffset? absoluteExpiration = null)
        {
            var doFetch = MakeWebRequest(new Uri(url), headers).SelectMany(x => ProcessWebResponse(x, url, absoluteExpiration));
            var fetchAndCache = doFetch.SelectMany(x => This.Insert(key, x, absoluteExpiration).Select(_ => x));

            var ret = default(IObservable<byte[]>);
            if (!fetchAlways)
            {
                ret = This.Get(key).Catch(fetchAndCache);
            }
            else 
            {
                ret = fetchAndCache;
            }

            var conn = ret.PublishLast();
            conn.Connect();
            return conn;
        }

        IObservable<byte[]> ProcessWebResponse(WebResponse wr, string url, DateTimeOffset? absoluteExpiration)
        {
            var hwr = (HttpWebResponse)wr;
            Debug.Assert(hwr != null, "The Web Response is somehow null but shouldn't be.");
            if ((int)hwr.StatusCode >= 400)
            {
                return Observable.Throw<byte[]>(new WebException(hwr.StatusDescription));
            }

            var ms = new MemoryStream();
            using (var responseStream = hwr.GetResponseStream())
            {
                Debug.Assert(responseStream != null, "The response stream is somehow null");
                responseStream.CopyTo(ms);
            }

            var ret = ms.ToArray();
            return Observable.Return(ret);
        }

        static IObservable<WebResponse> MakeWebRequest(
            Uri uri,
            IDictionary<string, string> headers = null,
            string content = null,
            int retries = 3,
            TimeSpan? timeout = null)
        {
            IObservable<WebResponse> request;

#if !SILVERLIGHT && !WINRT
            if (ModeDetector.InUnitTestRunner())
            {
                request = Observable.Defer(() =>
                {
                    var hwr = CreateWebRequest(uri, headers);

                    if (content == null)
                    {
                        return Observable.Start(() => hwr.GetResponse(), BlobCache.TaskpoolScheduler);
                    }

                    var buf = Encoding.UTF8.GetBytes(content);
                    return Observable.Start(() =>
                    {
                        hwr.GetRequestStream().Write(buf, 0, buf.Length);
                        return hwr.GetResponse();
                    }, BlobCache.TaskpoolScheduler);
                });
            }
            else 
#endif
            {
                request = Observable.Defer(() =>
                {
                    var hwr = CreateWebRequest(uri, headers);

                    if (content == null)
                    {
                        return Observable.FromAsyncPattern<WebResponse>(hwr.BeginGetResponse, hwr.EndGetResponse)();
                    }

                    var buf = Encoding.UTF8.GetBytes(content);

                    // NB: You'd think that BeginGetResponse would never block, 
                    // seeing as how it's asynchronous. You'd be wrong :-/
                    var ret = new AsyncSubject<WebResponse>();
                    Observable.Start(() =>
                    {
                        Observable.FromAsyncPattern<Stream>(hwr.BeginGetRequestStream, hwr.EndGetRequestStream)()
                            .SelectMany(x => x.WriteAsyncRx(buf, 0, buf.Length))
                            .SelectMany(_ => Observable.FromAsyncPattern<WebResponse>(hwr.BeginGetResponse, hwr.EndGetResponse)())
                            .Multicast(ret).Connect();
                    }, BlobCache.TaskpoolScheduler);

                    return ret;
                });
            }

            return request.Timeout(timeout ?? TimeSpan.FromSeconds(15), BlobCache.TaskpoolScheduler).Retry(retries);
        }

        static WebRequest CreateWebRequest(Uri uri, IDictionary<string, string> headers)
        {
            var hwr = WebRequest.Create(uri);
            if (headers != null)
            {
                foreach (var x in headers)
                {
                    hwr.Headers[x.Key] = x.Value;
                }
            }
            return hwr;
        }
    }
}
