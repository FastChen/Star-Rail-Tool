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

namespace Star_Rail_Tool
{
    public class GameHelper
    {
        static RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\miHoYo\崩坏：星穹铁道", true);
        const string GraphicsSettings_Model_h2986158309 = "GraphicsSettings_Model_h2986158309";
        public static void Unlock120FPS()
        {
            if(SetFPS(120, false))
            {
                SnackbarHelper.Show("Turbo 120 FPS!", "注意：120FPS 使用官方配置，PC解锁会影响游戏内的图像设置部分，建议修改好配置再解锁 120FPS", Wpf.Ui.Common.SymbolRegular.Fps12024, Wpf.Ui.Common.ControlAppearance.Danger);
            }
        }
        public static void Unlock60FPS()
        {
            if(SetFPS(60, false))
            {
                SnackbarHelper.Show("已修改为 60 FPS", "已将 FPS 上限修改为 60FPS", Wpf.Ui.Common.SymbolRegular.Fps6024, Wpf.Ui.Common.ControlAppearance.Secondary);
            }
        }

        private static bool SetFPS(int fps, bool vsync)
        {
            string data = GetGraphicsSettingsModelData();
            if (string.IsNullOrEmpty(data))
            {
                DialogHelper dialog = new DialogHelper();
                dialog.LeftButtonVisibility = System.Windows.Visibility.Hidden;
                dialog.RightButtonText = "我知道了";
                dialog.Title = "尝试修改 FPS 上限失败。";
                dialog.Content = "请尝试进入游戏图形设置，手动关闭 [垂直同步] 选项，然后关闭游戏后再试。";
                dialog.Show();

                //SnackbarHelper.ShowDanger("修改失败！", "原因：无法修改，请尝试进入游戏图形设置，手动关闭 [垂直同步] 选项，然后关闭游戏后再试。", 10000);
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
            }

            return true;
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
