using Star_Rail_Tool.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Appearance;
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
        private string title = "星穹之匙 | The key of Star Rail - FastChen";
        private string version = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static MainWindow mainWindows;
        //public NotifyIcon notifyIcon = new NotifyIcon();
        public IniFile settingini = new IniFile(SettingEntity.iniFilePath);
        public MainWindow()
        {
            InitializeComponent();
            SetTheme();
            SetWindowSize();

            InitializeUI();
            //TitleBar.Icon = new BitmapImage(new Uri(Properties.Resources.StarRail_NullCraft.ToString()));
            mainWindows = this;

            if (Core.IsAdministrator())
            {
                title = "[管理员] " + title;
            }

            title += " " + version;

            this.Title= title;
            TitleBar.Title = title;
        }

        private void InitializeUI()
        {
            //Wpf.Ui.Appearance.Accent.ApplySystemAccent();
            //Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.None);

            //Wpf.Ui.Appearance.Watcher.Watch(this);

        }

        private void SetTheme()
        {
            string theme = settingini.ReadValue("Software", "Theme");

            if (theme == "Dark")
            {
                Wpf.Ui.Appearance.Theme.Apply(ThemeType.Dark);
            }
            else
            {
                Wpf.Ui.Appearance.Theme.Apply(ThemeType.Light);
            }

            //if (theme == "Dark")
            //{
            //    Wpf.Ui.Appearance.Theme.Apply(ThemeType.Dark);
            //} else if (theme == "Light")
            //{
            //    Wpf.Ui.Appearance.Theme.Apply(ThemeType.Light);
            //}
            //else
            //{
            //   Wpf.Ui.Appearance.Watcher.Watch(this);
            //}
        }
        private void SetWindowSize()
        {
            string wWidth = settingini.ReadValue("Software", "WindowWidth");
            string wHeight = settingini.ReadValue("Software", "WindowHeight");
            if (!string.IsNullOrEmpty(wWidth))
            {
                this.Width = Convert.ToInt32(wWidth);
            }
            if (!string.IsNullOrEmpty(wHeight))
            {
                this.Height = Convert.ToInt32(wHeight);
            }
        }

        private void UiWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UiWindow_Closed(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                settingini.WriteValue("Software", "WindowWidth", ((int)Width).ToString());
                settingini.WriteValue("Software", "WindowHeight", ((int)Height).ToString());
            }
        }

        //private void NavigationButtonTheme_OnClick(object sender, RoutedEventArgs e)
        //{
        //    Wpf.Ui.Appearance.Theme.Apply(Theme.GetAppTheme() == Wpf.Ui.Appearance.ThemeType.Dark ? Wpf.Ui.Appearance.ThemeType.Light : Wpf.Ui.Appearance.ThemeType.Dark, Wpf.Ui.Appearance.BackgroundType.Mica);
        //    settingini.WriteValue("Software", "Theme", Theme.GetAppTheme().ToString());
        //}

        private void ActionDialog_OnButtonRightClick(object sender, RoutedEventArgs e)
        {
            RootDialog.Hide();
        }

    }
}
