using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;

namespace Star_Rail_Tool
{
    public class DialogHelper
    {
        Dialog dialog = MainWindow.mainWindows.RootDialog;
        public string Title { get; set; }
        public string Content { get; set; }
        public string LeftButtonText { get; set; }
        public Visibility LeftButtonVisibility { get; set; }
        public RoutedEventHandler LeftButtonClick { get; set; }
        public string RightButtonText { get; set; }
        public Visibility RightButtonVisibility { get; set; }
        public RoutedEventHandler RightButtonClick { get; set; }

        public void Show()
        {
            MainWindow.mainWindows.RootDialog_Title.Text= Title;
            MainWindow.mainWindows.RootDialog_Content.Text= Content;

            if (LeftButtonClick != null)
            {
                dialog.ButtonLeftClick += new RoutedEventHandler(LeftButtonClick);
            }
            if (RightButtonClick != null)
            {
                dialog.ButtonRightClick += new RoutedEventHandler(RightButtonClick);
            }

            dialog.ButtonLeftName = LeftButtonText;
            dialog.ButtonRightName = RightButtonText;

            dialog.ButtonLeftVisibility = LeftButtonVisibility;
            dialog.ButtonRightVisibility = RightButtonVisibility;

            dialog.Show();
        }

        private static void DialogControlOnButtonRightClick(object sender, RoutedEventArgs e)
        {
            var dialogControl = (IDialogControl)sender;
            dialogControl.Hide();
        }
        //public static void Show(string title, string massage, SymbolRegular icon, ControlAppearance appearance, int timeout = 5000)
        //{
        //    Snackbar RootSnackbar = MainWindow.mainWindows.RootSnackbar;
        //    RootSnackbar.Timeout = timeout;
        //    RootSnackbar.Show(title, massage, icon, appearance);
        //}
    }
}
