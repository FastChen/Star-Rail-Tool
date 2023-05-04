using Star_Rail_Tool.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Star_Rail_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        private string title = "星穹铁道工具 | 临时名称 参与问卷为软件命名 - FastChen";
        private string preVersion = "[Dev] 23n17d";
        public static MainWindow mainWindows;
        public MainWindow()
        {
            InitializeComponent();
            mainWindows = this;

            title = preVersion + " "+ title;
            if (Core.IsAdministrator())
            {
                title = "[管理员] " + title;
            }
            this.Title= title;
            TitleBar.Title = title;
        }

        private void UiWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NavigationButtonTheme_OnClick(object sender, RoutedEventArgs e)
        {
            var test = Wpf.Ui.Appearance.Theme.GetAppTheme();
            if (test == Wpf.Ui.Appearance.ThemeType.Dark)
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light, Wpf.Ui.Appearance.BackgroundType.Mica, true);
            }
            else
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark, Wpf.Ui.Appearance.BackgroundType.Mica, true);
            }

        }

        private void ActionDialog_OnButtonRightClick(object sender, RoutedEventArgs e)
        {
            RootDialog.Hide();
        }
    }
}
