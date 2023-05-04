using System;
using System.Windows;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Star_Rail_Tool.Pages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UiPage
    {
        private MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        IniFile settingini = new IniFile(SettingEntity.iniFilePath);
        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Run_Click(object sender, RoutedEventArgs e)
        {
            string path = settingini.ReadValue("Path", ((Button)sender).Tag.ToString());
            Core.RunGame(path);
        }

        private void ButtonAction_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not CardAction cardAction)
                return;

            var tag = cardAction.Tag as string;

            if (String.IsNullOrWhiteSpace(tag))
                return;

            switch (tag)
            {
                case "Join":
                    Core.OpenWebBrowser("http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=-xFlDbeV_RDgImiwyiJ9vHwh2edB_yFY&authKey=aFMhsPtdtsSVDQTRq%2F2rb5o%2FpH7UyAV1NN4sGfsRb478xoIy1Yi0u%2BD3yJ4MSrcH&noverify=0&group_code=495990072");
                    break;

                case "Github":
                    Core.OpenWebBrowser("https://github.com/fastchen");
                    break;
            }
        }


    }
}
