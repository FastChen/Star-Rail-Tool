using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Star_Rail_Tool.Pages
{
    /// <summary>
    /// AdvancedPage.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedPage : UiPage
    {
        public IniFile settingini = new IniFile(SettingEntity.iniFilePath);
        public AdvancedPage()
        {
            InitializeComponent();
            InitializeSetting();
        }

        private void InitializeSetting()
        {
            string mode = settingini.ReadValue("Game", "WindowMode");
            if (!string.IsNullOrEmpty(mode))
            {
                ComboBox_Window_Mode.SelectedIndex = Convert.ToInt32(settingini.ReadValue("Game", "WindowMode"));
            }

            NumberBox_Window_Width.Text = settingini.ReadValue("Game", "Width");
            NumberBox_Window_Height.Text = settingini.ReadValue("Game", "Height");
            TextBox_Window_ExtraParam.Text = settingini.ReadValue("Game", "ExtraParam");
        }

        private string WindowMode()
        {
            //-screen-fullscreen 0=关闭全屏独占 1=开启
            //ComboBox_WindowMode
            //0 全屏幕
            //1 窗口化
            //2 无边窗口化
            string param = "-screen-fullscreen";
            switch (ComboBox_Window_Mode.SelectedIndex)
            {
                case 0:
                    param += " 1";
                    break;
                case 1:
                    param += " 0";
                    break;
                case 2:
                    param = "-popupwindow";
                    break;
                default:
                    param = "";
                    break;
            }
            return param;
        }
        private string WindowsSize()
        {
            int width = Convert.ToInt32(NumberBox_Window_Width.Text);
            int heigth = Convert.ToInt32(NumberBox_Window_Height.Text);

            return $"-screen-width {width} -screen-height {heigth}";
        }
        private string GenerateParam()
        {
            string windowmode = WindowMode();
            string windowsize = WindowsSize();
            string fullParam = $"{windowmode} {windowsize} {TextBox_Window_ExtraParam.Text}";

            Console.WriteLine("[GameSettingForm]>[GenerateParam()]\r\n" + fullParam);
            return fullParam;
        }
        public void AdvancedStart()
        {
            string gamePath = settingini.ReadValue("Path", "Game");
            if (File.Exists(gamePath))
            {
                Core.RunGame(gamePath, GenerateParam());
            }
        }
        private void Button_Game_Run_Click(object sender, RoutedEventArgs e)
        {
            AdvancedStart();
        }

        private void Button_FPS_Modify(object sender, RoutedEventArgs e)
        {
            if (sender is not Wpf.Ui.Controls.Button button)
                return;

            var fps = button.Tag as string;

            if (fps == "120")
            {
                GameHelper.Unlock120FPS();
            }
            if (fps == "60")
            {
                GameHelper.Unlock60FPS();
            }
        }

        private void ComboBox_Window_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingini.WriteValue("Game", "WindowMode", ComboBox_Window_Mode.SelectedIndex.ToString());
        }

        private void NumberBox_Window_Size_TextChanged(object sender, TextChangedEventArgs e)
        {
            settingini.WriteValue("Game", ((NumberBox)sender).Tag.ToString(), ((NumberBox)sender).Text);
        }

        private void TextBox_Window_ExtraParam_TextChanged(object sender, TextChangedEventArgs e)
        {
            settingini.WriteValue("Game", "ExtraParam", TextBox_Window_ExtraParam.Text);
        }
    }
}
