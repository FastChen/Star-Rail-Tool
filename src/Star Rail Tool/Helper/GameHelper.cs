using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Services;

namespace Star_Rail_Tool
{
    public class GameHelper
    {
        static RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\miHoYo\崩坏：星穹铁道", true);


        public static void SetAudioVolumeSettings(int master, int bgm, int vo, int sfx)
        {
            try
            {
                regKey.SetValue("AudioSettings_MasterVolume_h1622207037", master);
                regKey.SetValue("AudioSettings_BGMVolume_h240914409", bgm);
                regKey.SetValue("AudioSettings_VOVolume_h805685304", vo);
                regKey.SetValue("AudioSettings_SFXVolume_h2753520268", sfx);

                SnackbarHelper.ShowSuccess("游戏设置已变更", "可进入游戏声音设置查看是否正确设置。");
            }
            catch (Exception ex)
            {
                SnackbarHelper.ShowDanger("修改音频设置失败！", "错误信息：" + ex.Message);
            }
        }

        public static int GetAudioVolumeSettings(string keyName)
        {
            int volume = -1;

            try
            {
                if (HasRegKey())
                {
                    volume = (int)regKey.GetValue(keyName);
                }
            }
            catch { }

            return volume;
        }


        const string GraphicsSettings_Model_h2986158309 = "GraphicsSettings_Model_h2986158309";
        public static string GetGraphicsSettingsModel()
        {
            string data = GetGraphicsSettingsModelData();
            return data;
        }

        public static void SetGraphicsSettingsModel(bool enableVSync, double renderScale, int shadowQuality, int reflectionQuality, int characterQuality, int envDetailQuality, int bloomQuality, int aAMode, int lightQuality)
        {
            string data = GetGraphicsSettingsModelData();
            if (string.IsNullOrEmpty(data))
            {
                DialogHelper dialog = new DialogHelper();
                dialog.LeftButtonVisibility = System.Windows.Visibility.Hidden;
                dialog.RightButtonText = "我知道了";
                dialog.Title = "尝试修改游戏配置失败。";
                dialog.Content = "请尝试进入游戏图形设置，手动关闭 [垂直同步] 选项，然后关闭游戏后再试。";
                dialog.Show();

                return;
            }

            var useData = JsonConvert.DeserializeObject<GraphicsSettingsModelEntity>(data);
            if (useData != null)
            {
                useData.EnableVSync = enableVSync;
                useData.RenderScale = renderScale;
                useData.ShadowQuality = shadowQuality;
                useData.ReflectionQuality = reflectionQuality;
                useData.CharacterQuality = characterQuality;
                useData.EnvDetailQuality = envDetailQuality;
                useData.BloomQuality = bloomQuality;
                useData.AAMode = aAMode;
                useData.LightQuality = lightQuality;

                byte[] newData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(useData));

                regKey.SetValue(GraphicsSettings_Model_h2986158309, newData);

                SnackbarHelper.ShowSuccess("游戏配置已变更", "可进入游戏图像设置查看是否正确设置。");

                Trace.WriteLine("SetGraphicsSettingsModel：");
                Trace.WriteLine("修改前：" + data);
                Trace.WriteLine("修改后：" + Encoding.UTF8.GetString(newData));
            }
        }

        public static int GetFPS()
        {
            string data = GetGraphicsSettingsModelData();
            int fps = -1; 
            if (string.IsNullOrEmpty(data))
            {
                return fps;
            }

            var useData = JsonConvert.DeserializeObject<GraphicsSettingsModelEntity>(data);
            if (useData != null)
            {
                fps = useData.FPS;
            }

            return fps;
        }

        public static void UnlockFPS(int fps)
        {
            switch (fps)
            {
                case 120:
                    if (SetFPS(120, false))
                    {
                        SnackbarHelper.Show("Turbo 120 FPS! 已解锁！", "注意：120FPS 使用官方功能实现，解锁会影响游戏内的图像设置，可以使用软件修改图像设置。", Wpf.Ui.Common.SymbolRegular.Fps12024, Wpf.Ui.Common.ControlAppearance.Danger);
                    }
                    break;
                case 60:
                    if (SetFPS(60, false))
                    {
                        SnackbarHelper.Show("已修改为 60 FPS", "已将 FPS 上限修改为 60FPS", Wpf.Ui.Common.SymbolRegular.Fps6024, Wpf.Ui.Common.ControlAppearance.Caution);
                    }
                    break;
                case 30:
                    if (SetFPS(30, false))
                    {
                        SnackbarHelper.Show("已修改为 30 FPS", "已将 FPS 上限修改为 30FPS", Wpf.Ui.Common.SymbolRegular.AnimalTurtle24, Wpf.Ui.Common.ControlAppearance.Secondary);
                    }
                    break;
            }
        }

        private static void PopupErrorDialog()
        {
            DialogHelper dialog = new DialogHelper();
            dialog.LeftButtonVisibility = System.Windows.Visibility.Hidden;
            dialog.RightButtonText = "我知道了";
            dialog.Title = "尝试修改 FPS 上限失败。";
            dialog.Content = "请尝试进入游戏图形设置，手动关闭 [垂直同步] 选项，然后关闭游戏后再试。";
            dialog.Show();
        }

        private static bool SetFPS(int fps, bool vsync)
        {
            string data = GetGraphicsSettingsModelData();
            if (string.IsNullOrEmpty(data))
            {
                PopupErrorDialog();
                return false;
            }

            var useData = JsonConvert.DeserializeObject<GraphicsSettingsModelEntity>(data);
            if(useData != null)
            {
                useData.FPS = fps;
                useData.EnableVSync = vsync;

                byte[] newData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(useData));

                regKey.SetValue(GraphicsSettings_Model_h2986158309, newData);

                Trace.WriteLine("修改前：" + data);
                Trace.WriteLine("修改后：" + Encoding.UTF8.GetString(newData));

                return true;
            }
            else
            {
                PopupErrorDialog();
            }
            return false;
        }

        private static bool HasRegKey()
        {
            return regKey != null;
        }

        private static string GetGraphicsSettingsModelData()
        {
            string data = null;
            try
            {
                if (HasRegKey())
                {
                    data = Encoding.UTF8.GetString((byte[])regKey.GetValue(GraphicsSettings_Model_h2986158309));
                }
            }
            catch{ }
            return data;
        }

    }
}
