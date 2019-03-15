using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace UniVoting.Services
{
    public interface IDialogService
    {
        Task<MessageDialogResult> AskQuestionAsync(MetroWindow metro,string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(MetroWindow metroWindow,string title, string message);
        Task ShowMessageAsync(MetroWindow metroWindow,string title, string message);
    }

    internal class DialogService : IDialogService
    {
        private MetroWindow _metroWindow;

        //public DialogService(MetroWindow metro)
        //{
        //    this.metro = metro;
        //}

        public Task<MessageDialogResult> AskQuestionAsync(MetroWindow metro,string title, string message)
        {
            this._metroWindow = metro;
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            return metro.ShowMessageAsync(title, message, 
                MessageDialogStyle.AffirmativeAndNegative, settings);
        }

        public Task<ProgressDialogController> ShowProgressAsync(MetroWindow metro, string title, string message)
        {
            this._metroWindow = metro;

            return _metroWindow.ShowProgressAsync(title, message);
        }

        public Task ShowMessageAsync(MetroWindow metro, string title, string message)
        {
            this._metroWindow = metro;
            return _metroWindow.ShowMessageAsync(title, message);
        }
    }
}