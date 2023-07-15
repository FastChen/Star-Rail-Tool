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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Titanium.Web.Proxy.Models;
using Titanium.Web.Proxy;
using Wpf.Ui.Controls;
using Titanium.Web.Proxy.EventArguments;
using System.Net;

namespace Star_Rail_Tool.Pages
{
    /// <summary>
    /// GetWarpLinkPage.xaml 的交互逻辑
    /// </summary>
    public partial class GetWarpLinkPage : UiPage
    {
        private ProxyServer proxyServer;
        private ExplicitProxyEndPoint explicitEndPoint;
        public GetWarpLinkPage()
        {
            InitializeComponent();
        }

        private void Button_Proxy_Start_Click(object sender, RoutedEventArgs e)
        {
            StartProxy();
        }

        private void Button_Proxy_Stop_Click(object sender, RoutedEventArgs e)
        {
            StopProxy();
        }

        private void Button_Remove_Certificate_Click(object sender, RoutedEventArgs e)
        {
            if (proxyServer != null)
            {
                proxyServer.CertificateManager.RemoveTrustedRootCertificate();
                FileHelper.Delete(Environment.CurrentDirectory + @"\rootCert.pfx");
                SnackbarHelper.ShowSuccess("成功", "已移除证书并删除文件");
            }
        }


        private void StartProxy()
        {
            proxyServer = new ProxyServer();
            proxyServer.BeforeRequest += OnRequest;

            //绑定监听端口
            explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8086, true);
            Console.WriteLine("监听地址127.0.0.1:8086");
            //代理服务器注册监听地址
            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

            UpdateControlStatus();

            SnackbarHelper.ShowSuccess("已启动跃迁链接获取代理", "代理状态更新");
        }

        private void StopProxy()
        {
            if (proxyServer.ProxyRunning)
            {
                proxyServer.Stop();
            }
            UpdateControlStatus();

            SnackbarHelper.ShowInfo("已关闭跃迁链接获取代理", "代理状态更新");
        }

        private async void AddLog(string str)
        {
            RichTextBox_Log.AppendText(str + "\r\n");
        }
        private void UpdateControlStatus()
        {
            Button_Remove_Certificate.IsEnabled = true;
            Button_Proxy_Start.IsEnabled = !proxyServer.ProxyRunning;
            Button_Proxy_Stop.IsEnabled = proxyServer.ProxyRunning;
        }

        public async Task OnRequest(object sender, SessionEventArgs e)
        {
            //Console.WriteLine(e.HttpClient.Request.Url);
            var method = e.HttpClient.Request.Method.ToUpper();
            if (method == "GET")
            {
                string requestUrl = e.HttpClient.Request.Url;
                string targetUrl = "https://api-takumi.mihoyo.com/common/gacha_record/api/getGachaLog?";
                if (requestUrl.Contains(targetUrl))
                {
                    Console.WriteLine("已截取AuthKey URL:" + requestUrl);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        CradExpander_Link.IsExpanded = true;
                        AddLog(requestUrl);
                        StopProxy();

                        SnackbarHelper.ShowInfo("成功获取跃迁链接", "请在步骤三内的文本框中查看或复制");
                    }));

                }
            }
        }
    }
}
