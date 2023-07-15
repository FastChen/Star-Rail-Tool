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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Services;

namespace Star_Rail_Tool.Controls
{
    /// <summary>
    /// AudioSettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class AudioSettingsControl : UserControl
    {
        public AudioSettingsControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReadAudioVolumeSettings();
        }

        private void ReadAudioVolumeSettings()
        {
            Slider_Volume_Master.Value = GameHelper.GetAudioVolumeSettings("AudioSettings_MasterVolume_h1622207037");
            Slider_Volume_BGM.Value = GameHelper.GetAudioVolumeSettings("AudioSettings_BGMVolume_h240914409");
            Slider_Volume_VO.Value = GameHelper.GetAudioVolumeSettings("AudioSettings_VOVolume_h805685304");
            Slider_Volume_SFX.Value = GameHelper.GetAudioVolumeSettings("AudioSettings_SFXVolume_h2753520268");
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            DialogHelper dialog = new DialogHelper();
            dialog.LeftButtonVisibility = System.Windows.Visibility.Visible;
            dialog.LeftButtonClick += new RoutedEventHandler(DialogLeftButtonClick);
            dialog.LeftButtonText = "确定修改";
            dialog.RightButtonText = "取消";
            dialog.Title = "确定修改游戏声音设置？";
            dialog.Content = "如果修改失败，尝试以管理员模式启动工具并关闭游戏后再试。";
            dialog.Show();
        }

        private void DialogLeftButtonClick(object sender, RoutedEventArgs e)
        {
            var dialogControl = (IDialogControl)sender;
            dialogControl.Hide();

            GameHelper.SetAudioVolumeSettings((int)Slider_Volume_Master.Value, (int)Slider_Volume_BGM.Value, (int)Slider_Volume_VO.Value, (int)Slider_Volume_SFX.Value);
        }

    }
}
