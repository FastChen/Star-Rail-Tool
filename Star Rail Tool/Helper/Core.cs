using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Rail_Tool
{
    public class Core
    {
        public static bool IsAdministrator()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static void RunGame(string path, string param = "")
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();

                    throw new SystemException();
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = path;
                startInfo.Arguments = param;
                startInfo.WorkingDirectory = Path.GetDirectoryName(path);

                Process.Start(startInfo);

                SnackbarHelper.Show("启动文件中...", "当前启动文件路径：" + path, Wpf.Ui.Common.SymbolRegular.Open24, Wpf.Ui.Common.ControlAppearance.Primary);
            }
            catch (Win32Exception w)
            {
                SnackbarHelper.ShowDanger("需要管理员权限", "错误信息：" + w.Message);
            }
            catch (Exception e)
            {
                string eMessage = e.Message;
                if (e.GetType() == typeof(FileNotFoundException))
                {
                    eMessage = "文件不存在，请前往 [本体设置] 重新设置启动器路径。";
                }
                SnackbarHelper.ShowDanger("启动失败...", "错误信息：" + eMessage);
            }
        }

        public static void OpenWebBrowser(string url)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = url;
            proc.Start();
        }
    }
}
