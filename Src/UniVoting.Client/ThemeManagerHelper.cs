using System.Windows;
using Wpf.Ui.Appearance;

namespace UniVoting.Client
{
    public static class ThemeManagerHelper
    {
        public static void Apply(ApplicationTheme theme = ApplicationTheme.Light)
        {
            ApplicationThemeManager.Apply(theme);
        }
    }
}
