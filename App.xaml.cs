using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Star_Rail_Tool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string eType = "";
        string eMessage = "";
        string eStackTrace = "";
        public App()
        {
            //首先注册开始和退出事件
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //UI线程未捕获异常处理事件
            //this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            //程序退出时需要处理的业务
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                                  //MessageBox.Show("UI线程异常:" + e.Exception.Message);

                eType = "异常类型；UI线程异常";
                eMessage = "异常信息：" + e.Exception.Message;
                eStackTrace = "异常全部信息：" + e.Exception.StackTrace;
                MessageBox.Show(eType + "\r\n" + eMessage + "\r\n" + eStackTrace, "请将此界面截图并反馈给开发者-快辰，已捕获得到的异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                //if (ex.InnerException != null)
                //    Console.WriteLine("Inner exception: {0}", e.InnerException);

                MessageBox.Show("特殊UI线程异常：" + ex.InnerException);
                //此时程序出现严重异常，将强制结束退出
                MessageBox.Show("UI线程发生致命错误！\r\n请把发生此错误之前做的事情反馈给开发者-快辰", "无法继续运行");
            }

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("异常类型；非UI线程发生致命错误");
            }
            sbEx.Append("异常类型；非UI线程异常");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            MessageBox.Show(sbEx.ToString(), "请将此界面截图并反馈给开发者-快辰，已捕获得到的异常", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            eType = "异常类型；Task线程异常";
            eMessage = "异常信息：" + e.Exception.Message;
            eStackTrace = "异常全部信息：" + e.Exception.StackTrace;
            MessageBox.Show(eType + "\r\n" + eMessage + "\r\n" + eStackTrace, "请将此界面截图并反馈给开发者-快辰，已捕获得到的异常", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }
    }
}
