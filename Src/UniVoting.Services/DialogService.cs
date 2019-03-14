using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Univoting.Services
{
    public interface IDialogService
    {
        Task<MessageDialogResult> ShowDialogAsync(string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);
    }

    public class DialogService : IDialogService
    {
        private readonly MetroWindow _metroWindow;

        public DialogService(MetroWindow metroWindow)
        {
            this._metroWindow = metroWindow;
        }

        public Task<MessageDialogResult> ShowDialogAsync(string title, string message)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            return _metroWindow.ShowMessageAsync(title, message, 
                MessageDialogStyle.AffirmativeAndNegative, settings);
        }

        public Task<ProgressDialogController> ShowProgressAsync(string title, string message)
        {
            return _metroWindow.ShowProgressAsync(title, message);
        }

        public Task ShowMessageAsync(string title, string message)
        {
            return _metroWindow.ShowMessageAsync(title, message);
        }


        public static MetroDialogSettings MetroDialogSettings()
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            return settings;
        }
    }
}