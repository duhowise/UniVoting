using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace UniVoting.Admin
{

    internal static class ThemeManagerHelper
    {
        public static void CreateAppStyleBy(Color color, bool changeImmediately = false)
        {
            // create a runtime accent resource dictionary

            var resourceDictionary = new ResourceDictionary
            {
                {"HighlightColor", color},
                {"AccentBaseColor", color},
                {"AccentColor", Color.FromArgb((byte) (204), color.R, color.G, color.B)},
                {"AccentColor2", Color.FromArgb((byte) (153), color.R, color.G, color.B)},
                {"AccentColor3", Color.FromArgb((byte) (102), color.R, color.G, color.B)},
                {"AccentColor4", Color.FromArgb((byte) (51), color.R, color.G, color.B)}
            };


            resourceDictionary.Add("HighlightBrush", GetSolidColorBrush((Color) resourceDictionary["HighlightColor"]));
            resourceDictionary.Add("AccentBaseColorBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentBaseColor"]));
            resourceDictionary.Add("AccentColorBrush", GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("AccentColorBrush2", GetSolidColorBrush((Color) resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("AccentColorBrush3", GetSolidColorBrush((Color) resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("AccentColorBrush4", GetSolidColorBrush((Color) resourceDictionary["AccentColor4"]));
            resourceDictionary.Add("WindowTitleColorBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));

            resourceDictionary.Add("ProgressBrush", new LinearGradientBrush(
                new GradientStopCollection(new[]
                {
                    new GradientStop((System.Windows.Media.Color) resourceDictionary["HighlightColor"], 0),
                    new GradientStop((System.Windows.Media.Color) resourceDictionary["AccentColor3"], 1)
                }),
                new Point(0.001, 0.5), new Point(1.002, (int) 0.5)));

            resourceDictionary.Add("CheckmarkFill", GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("RightArrowFill", GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));

            resourceDictionary.Add("IdealForegroundColor", IdealTextColor(color));
            resourceDictionary.Add("IdealForegroundColorBrush",
                GetSolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("IdealForegroundDisabledBrush",
                GetSolidColorBrush((Color) resourceDictionary["IdealForegroundColor"], 0.4));
            resourceDictionary.Add("AccentSelectedColorBrush",
                GetSolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));

            resourceDictionary.Add("MetroDataGrid.HighlightBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.HighlightTextBrush",
                GetSolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("MetroDataGrid.MouseOverHighlightBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("MetroDataGrid.FocusBorderBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightBrush",
                GetSolidColorBrush((Color) resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightTextBrush",
                GetSolidColorBrush((Color) resourceDictionary["IdealForegroundColor"]));

            if (changeImmediately)
            {
                // Apply WPF UI theme system instead of MahApps Metro ThemeManager
                var application = Application.Current;
                if (application != null)
                {
                    // Apply the modern WPF UI theme
                    if (application.MainWindow != null)
                    {
                        Wpf.Ui.Appearance.ApplicationThemeManager.Apply(application.MainWindow);
                    }
                    
                    // Apply accent colors to application resources
                    ApplyAccentToApplication(application, color, resourceDictionary);
                }
            }
        }

        /// <summary>
        /// Apply accent color to application resources for WPF UI compatibility
        /// </summary>
        /// <param name="application">The WPF application</param>
        /// <param name="color">The accent color</param>
        /// <param name="resourceDictionary">The resource dictionary to merge</param>
        private static void ApplyAccentToApplication(Application application, Color color, ResourceDictionary resourceDictionary)
        {
            try
            {
                // Merge the resource dictionary into application resources
                application.Resources.MergedDictionaries.Add(resourceDictionary);
                
                // Set WPF UI compatible accent colors
                application.Resources["SystemAccentColor"] = color;
                application.Resources["SystemAccentColorLight1"] = Color.FromArgb(153, color.R, color.G, color.B);
                application.Resources["SystemAccentColorLight2"] = Color.FromArgb(102, color.R, color.G, color.B);
                
                // Create brushes for WPF UI
                application.Resources["SystemAccentColorBrush"] = GetSolidColorBrush(color);
                application.Resources["SystemAccentColorLight1Brush"] = GetSolidColorBrush(Color.FromArgb(153, color.R, color.G, color.B));
                application.Resources["SystemAccentColorLight2Brush"] = GetSolidColorBrush(Color.FromArgb(102, color.R, color.G, color.B));
                
                // WPF UI primary colors
                application.Resources["ApplicationAccentColorPrimary"] = GetSolidColorBrush(color);
                application.Resources["ApplicationAccentColorSecondary"] = GetSolidColorBrush(Color.FromArgb(153, color.R, color.G, color.B));
                application.Resources["ApplicationAccentColorTertiary"] = GetSolidColorBrush(Color.FromArgb(102, color.R, color.G, color.B));
            }
            catch (Exception ex)
            {
                // Log error but don't crash the application
                System.Diagnostics.Debug.WriteLine($"Error applying accent color: {ex.Message}");
            }
        }

        /// <summary>
        /// Determining Ideal Text Color Based on Specified Background Color
        /// http://www.codeproject.com/KB/GDI-plus/IdealTextColor.aspx
        /// </summary>
        /// <param name = "color">The bg.</param>
        /// <returns></returns>
        private static Color IdealTextColor(Color color)
        {
            const int nThreshold = 105;
            var bgDelta = System.Convert.ToInt32((color.R*0.299) + (color.G*0.587) + (color.B*0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        private static SolidColorBrush GetSolidColorBrush(Color color, double opacity = 1d)
        {
            var brush = new SolidColorBrush(color) {Opacity = opacity};
            brush.Freeze();
            return brush;
        }
    }
}
