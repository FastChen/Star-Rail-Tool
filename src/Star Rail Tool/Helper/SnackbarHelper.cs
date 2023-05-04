using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Contracts;

namespace Star_Rail_Tool
{
    public class SnackbarHelper
    {
        //Snackbar RootSnackbar = MainWindow.mainWindows.RootSnackbar;
        //public string Title { get; set; }
        //public string Message { get; set; }
        //public int Timeout { get; set; }
        //public SymbolRegular Icon { get; set; }
        //public ControlAppearance Appearance { get; set; }

        //private int GetTimeout()
        //{
        //    if (Timeout == default(int))
        //    {
        //        Timeout = 5000;
        //    }
        //    return Timeout;
        //}

        //public void Show()
        //{
        //    RootSnackbar.Timeout = GetTimeout();
        //    RootSnackbar.Show(Title, Message, Icon, Appearance);
        //}

        /// <summary>
        /// 显示底部通知，调用 MainWindow 内的 RootSnackbar 控件。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="massage">内容</param>
        /// <param name="icon">图标</param>
        /// <param name="appearance">主题色</param>
        /// <param name="timeout">消失时间</param>
        public static void Show(string title, string massage, SymbolRegular icon, ControlAppearance appearance, int timeout = 5000)
        {
            Snackbar RootSnackbar = MainWindow.mainWindows.RootSnackbar;
            RootSnackbar.Timeout = timeout;
            RootSnackbar.Show(title, massage, icon, appearance);
        }
        public static void ShowInfo(string title, string massage, int timeout = 5000)
        {
            Show(title, massage, SymbolRegular.Info24, ControlAppearance.Info, timeout);
        }
        public static void ShowSuccess(string title, string massage, int timeout = 5000)
        {
            Show(title, massage, SymbolRegular.CheckmarkCircle24, ControlAppearance.Success, timeout);
        }
        public static void ShowCaution(string title, string massage, int timeout = 5000)
        {
            Show(title, massage, SymbolRegular.ErrorCircle24, ControlAppearance.Caution, timeout);
        }
        public static void ShowDanger(string title, string massage, int timeout = 5000)
        {
            Show(title, massage, SymbolRegular.DismissCircle24, ControlAppearance.Danger, timeout);
        }
    }
}
