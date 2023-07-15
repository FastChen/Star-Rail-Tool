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

        private string GetConfigPath(string path)
        {
            string temp = Path.GetDirectoryName(path) + @"\config.ini";
            if (FileHelper.Exists(temp))
            {
                return temp;
            }
            return null;
        }

        private void InitallzeChannel()
        {
            string launcherConfigPath = GetConfigPath(settingini.ReadValue("Path", "Launcher"));
            string gameConfigPath = GetConfigPath(settingini.ReadValue("Path", "Game"));

            if (FileHelper.Exists(gameConfigPath))
            {
                channel = new ChannelHelper(launcherConfigPath, gameConfigPath);

                ComboBox_Channel.Items.Clear();
                ComboBox_Channel.ItemsSource = channel.GetChannelNames();

                ReadGameServer();
            }
        }

        public void ReadGameServer()
        {
            if (channel != null)
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
            else
            {
                InitallzeChannel();
            }
        }

        private void Button_Channel_Switch_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Channel.SelectedIndex != -1)
            {
                channel.Set(ComboBox_Channel.SelectedIndex);
                ReadGameServer();
            }

        }


        //SaveAccount save = new SaveAccount();
        //private void Button_Test_A_Click(object sender, RoutedEventArgs e)
        //{

        //    save.SaveAccountFile("TEST");
        //}

        //private void Button_Test_B_Click(object sender, RoutedEventArgs e)
        //{
        //    save.WriteAccountRegKey("TEST");
        //}
    }
}
