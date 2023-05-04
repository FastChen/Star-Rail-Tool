using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace Star_Rail_Tool.Pages
{
    /// <summary>
    /// ChannelSwitchPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelSwitchPage : UiPage
    {
        IniFile settingini = new IniFile(SettingEntity.iniFilePath);
        ChannelHelper channel;

        string lastServer = string.Empty;
        public ChannelSwitchPage()
        {
            InitializeComponent();
            InitallzeChannel();
        }

        private void UiPage_Loaded(object sender, RoutedEventArgs e)
        {
            ReadGameServer();
        }

        private void InitallzeChannel()
        {
            string launcherPath = settingini.ReadValue("Path", "Launcher");
            string gamePath = settingini.ReadValue("Path", "Game");

            channel = new ChannelHelper(launcherPath, gamePath);

            ComboBox_Channel.Items.Clear();
            ComboBox_Channel.ItemsSource = channel.GetChannelNames();

            ReadGameServer();
        }

        public void ReadGameServer()
        {
            int serverIndex = channel.Get();

            if (serverIndex != -1)
            {
                ComboBox_Channel.SelectedIndex = serverIndex;
                TextBlock_Channel_Type.Text = "当前登录服务器：" + ComboBox_Channel.Text;

                if (ComboBox_Channel.Text != lastServer)
                {
                    SnackbarHelper.ShowCaution("登录服务器状态更新", "当前登录服务器：" + ComboBox_Channel.Text);
                }
            }
            lastServer = ComboBox_Channel.Text;
        }

        private void Button_Channel_Switch_Click(object sender, RoutedEventArgs e)
        {
            channel.Set(ComboBox_Channel.SelectedIndex);
            ReadGameServer();
        }
        SaveAccount save = new SaveAccount();
        private void Button_Test_A_Click(object sender, RoutedEventArgs e)
        {

            save.SaveAccountFile("TEST");
        }

        private void Button_Test_B_Click(object sender, RoutedEventArgs e)
        {
            save.WriteAccountRegKey("TEST");
        }
    }
}
