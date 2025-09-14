using System;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Appearance;

namespace UniVoting.Client
{
    /// <summary>
    /// Helper class for managing application themes and colors using WPF UI
    /// Replaces the old MahApps.Metro ThemeManager functionality
    /// </summary>
    public static class ThemeManagerHelper
    {
        /// <summary>
        /// Apply a color scheme to the current application using WPF UI theming
        /// </summary>
        /// <param name="color">The accent color to apply</param>
        /// <param name="changeImmediately">Whether to apply the theme immediately</param>
        public static void CreateAppStyleBy(Color color, bool changeImmediately = false)
        {
            if (changeImmediately)
            {
                // Apply WPF UI theme with custom accent color
                var application = Application.Current;
                if (application != null)
                {
                    // Use WPF UI's theme system
                    if (application.MainWindow != null)
                    {
                        SystemThemeWatcher.Watch(application.MainWindow);
                    }
                    
                    // Apply the theme to the main window
                    if (application.MainWindow != null)
                    {
                        ApplicationThemeManager.Apply(application.MainWindow);
                    }
                    
                    // Set custom accent color in application resources
                    SetAccentColor(application, color);
                }
            }
        }

        /// <summary>
        /// Set a custom accent color in the application resources
        /// </summary>
        /// <param name="application">The WPF application</param>
        /// <param name="color">The accent color</param>
        private static void SetAccentColor(Application application, Color color)
        {
            try
            {
                // Create accent color variations
                var accentColor = color;
                var accentColor2 = Color.FromArgb(153, color.R, color.G, color.B);
                var accentColor3 = Color.FromArgb(102, color.R, color.G, color.B);
                
                // Update application resources with custom accent colors
                application.Resources["SystemAccentColor"] = accentColor;
                application.Resources["SystemAccentColorLight1"] = accentColor2;
                application.Resources["SystemAccentColorLight2"] = accentColor3;
                
                // Create brushes
                application.Resources["SystemAccentColorBrush"] = new SolidColorBrush(accentColor);
                application.Resources["SystemAccentColorLight1Brush"] = new SolidColorBrush(accentColor2);
                application.Resources["SystemAccentColorLight2Brush"] = new SolidColorBrush(accentColor3);
                
                // Set ideal foreground color
                var idealForeground = IdealTextColor(color);
                application.Resources["IdealForegroundColor"] = idealForeground;
                application.Resources["IdealForegroundColorBrush"] = new SolidColorBrush(idealForeground);
                
                // Legacy compatibility brushes for existing code
                application.Resources["AccentColorBrush"] = new SolidColorBrush(accentColor);
                application.Resources["AccentColorBrush2"] = new SolidColorBrush(accentColor2);
                application.Resources["AccentColorBrush3"] = new SolidColorBrush(accentColor3);
                application.Resources["HighlightBrush"] = new SolidColorBrush(accentColor);
            }
            catch (Exception ex)
            {
                // Log error but don't crash the application
                System.Diagnostics.Debug.WriteLine($"Error setting accent color: {ex.Message}");
            }
        }

        /// <summary>
        /// Determine ideal text color based on background color
        /// </summary>
        /// <param name="backgroundColor">The background color</param>
        /// <returns>Black or White color for optimal contrast</returns>
        private static Color IdealTextColor(Color backgroundColor)
        {
            const int threshold = 105;
            var bgDelta = Convert.ToInt32((backgroundColor.R * 0.299) + (backgroundColor.G * 0.587) + (backgroundColor.B * 0.114));
            return (255 - bgDelta < threshold) ? Colors.Black : Colors.White;
        }

        /// <summary>
        /// Create a frozen SolidColorBrush with the specified color and opacity
        /// </summary>
        /// <param name="color">The color</param>
        /// <param name="opacity">The opacity (0.0 to 1.0)</param>
        /// <returns>A frozen SolidColorBrush</returns>
        private static SolidColorBrush GetSolidColorBrush(Color color, double opacity = 1.0)
        {
            var brush = new SolidColorBrush(color) { Opacity = opacity };
            brush.Freeze();
            return brush;
        }

        /// <summary>
        /// Switch between Light and Dark themes
        /// </summary>
        /// <param name="useDarkTheme">True for dark theme, false for light theme</param>
        public static void SetTheme(bool useDarkTheme)
        {
            var theme = useDarkTheme ? ApplicationTheme.Dark : ApplicationTheme.Light;
            ApplicationThemeManager.Apply(theme);
        }

        /// <summary>
        /// Apply theme to a specific window
        /// </summary>
        /// <param name="window">The window to apply theme to</param>
        public static void ApplyThemeToWindow(Window window)
        {
            if (window != null)
            {
                ApplicationThemeManager.Apply(window);
            }
        }
    }
}
