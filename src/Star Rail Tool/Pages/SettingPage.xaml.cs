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
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
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

            List<ThemeSettingEntity> theme = new List<ThemeSettingEntity>();
            //theme.Add(new ThemeSettingEntity { ID = 0, Name = "跟随系统", Value="Auto" });
            theme.Add(new ThemeSettingEntity { ID = 1, Name = "深色", Value = "Dark" });
            theme.Add(new ThemeSettingEntity { ID = 2, Name = "浅色", Value = "Light" });
            ComboBox_Theme.ItemsSource = theme;
        }

        public void ReadSetting()
        {
            string launcherPath = settingini.ReadValue("Path", "Launcher");
            string gamePath = settingini.ReadValue("Path", "Game");

            string theme = settingini.ReadValue("Software", "Theme");

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

            if (!string.IsNullOrEmpty(theme))
            {
                ComboBox_Theme.SelectedValue = theme;
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
                TextBox_Launcher_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
                TextBox_Launcher_Path.Text = dialog.FileName;

                string gamePath = GetGamePath(dialog.FileName);
                if (FileHelper.Exists(gamePath))
                {
                    WriteGamePath(gamePath);
                }
                else
                {
                    DialogHelper findDialog = new DialogHelper();
                    findDialog.LeftButtonVisibility = Visibility.Hidden;
                    findDialog.RightButtonText = "手动设置";
                    findDialog.RightButtonClick = new RoutedEventHandler(DialogRightButtonClick);
                    findDialog.Title = "尝试自动设置游戏路径失败。";
                    findDialog.Content = "请点击下方 [手动设置] 按钮进行手动选择《崩坏：星穹铁道》游戏本体(StarRail.exe)";
                    findDialog.Show();
                }
            }
        }

        private void WriteGamePath(string gamePath)
        {
            settingini.WriteValue("Path", "Game", gamePath);
            TextBox_Game_Path.Text = gamePath;
            TextBox_Game_Path.Icon = Wpf.Ui.Common.SymbolRegular.CheckmarkCircle24;
            
            if (FileHelper.Exists(gamePath))
            {
                SnackbarHelper.ShowSuccess("游戏路径设置完成", "已成功设置游戏路径。");
            }
        }

        private void DialogRightButtonClick(object sender, RoutedEventArgs e)
        {
            var dialogControl = (IDialogControl)sender;
            dialogControl.Hide();

            WriteGamePath(OpenFindGameDialog());
        }

        private string OpenFindGameDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择《崩坏：星穹铁道》游戏本体(StarRail.exe)";
            dialog.Filter = "星穹铁道本体|StarRail.exe";

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return "";
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

                Trace.WriteLine(launcherConfigPath+" | "+ gamePath+" | "+ gameName);

                if (File.Exists(fullGamePath))
                {
                    return fullGamePath;
                }
            }
            return "";
        }


        private void ComboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO
        }

        private void ComboBox_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string theme = ComboBox_Theme.SelectedValue.ToString();

            if (theme == "Dark")
            {
                Wpf.Ui.Appearance.Theme.Apply(ThemeType.Dark);
            }
            else
            {
                Wpf.Ui.Appearance.Theme.Apply(ThemeType.Light);
            }
            settingini.WriteValue("Software", "Theme", theme);

        }

    }
}
