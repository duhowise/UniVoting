using System.Windows;
using Wpf.Ui.Appearance;

namespace UniVoting.LiveView
{
    internal static class ThemeManagerHelper
    {
        public static void Apply(ApplicationTheme theme = ApplicationTheme.Light)
        {
            ApplicationThemeManager.Apply(theme);
        }
    }
}
