using System.Windows;
using Wpf.Ui.Appearance;

namespace UniVoting.Admin
{
    internal static class ThemeManagerHelper
    {
        public static void Apply(ApplicationTheme theme = ApplicationTheme.Light)
        {
            ApplicationThemeManager.Apply(theme);
        }
    }
}
