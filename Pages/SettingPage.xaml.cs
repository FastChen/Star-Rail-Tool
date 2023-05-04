using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Wpf.Ui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Star_Rail_Tool.Pages
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : UiPage
    {
        IniFile settingini = new IniFile(SettingEntity.iniFilePath);
        public SettingPage()
        {
            InitializeComponent();
            ReadSetting();
        }

        public void ReadSetting()
        {
            string launcherPath = settingini.ReadValue("Path", "Launcher");
            string gamePath = settingini.ReadValue("Path", "Game");
            if (FileHelper.Exists(launcherPath))
            {
                TextBox_Launcher_Path.Text = launcherPath;
                TextBox_Launcher_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
            }
            if (FileHelper.Exists(gamePath))
            {
                TextBox_Game_Path.Text = gamePath;
                TextBox_Game_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
            }
            else
            {
                string getGamePath = GetGamePath(launcherPath);
                if (!string.IsNullOrEmpty(getGamePath))
                {
                    TextBox_Game_Path.Text = getGamePath;
                    TextBox_Game_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
                }
            }

        }

        private void Button_Path_Launcher_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择《崩坏：星穹铁道》的启动器文件(launcher.exe)";
            dialog.Filter = "星穹铁道启动器|launcher.exe";
            
            if (dialog.ShowDialog() == true)
            {
                settingini.WriteValue("Path", "Launcher", dialog.FileName);
                string gamePath = GetGamePath(dialog.FileName);
                settingini.WriteValue("Path", "Game", gamePath);

                TextBox_Launcher_Path.Text = dialog.FileName;
                TextBox_Game_Path.Text = gamePath;
                TextBox_Launcher_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
                TextBox_Game_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;

                SnackbarHelper.ShowSuccess("成功", "已成功设置路径。");

                Trace.WriteLine(dialog.FileName + " | " + gamePath);
            }
        }

        public string GetGamePath(string launcherPath)
        {
            string launcherConfigPath = Path.GetDirectoryName(launcherPath)+@"\config.ini";
            if (File.Exists(launcherConfigPath))
            {
                IniFile launcherConfig = new IniFile(launcherConfigPath);
                string gamePath = launcherConfig.ReadValue("launcher", "game_install_path").Replace("/", @"\");
                string gameName = launcherConfig.ReadValue("launcher", "game_start_name");
                string fullGamePath = gamePath + @"\" + gameName;

                if (File.Exists(fullGamePath))
                {
                    return fullGamePath;
                }
            }
            return "";
        }
    }
}
