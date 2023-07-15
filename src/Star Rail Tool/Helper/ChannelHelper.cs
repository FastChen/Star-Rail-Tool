using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Star_Rail_Tool
{
    public class ChannelHelper
    {
        private List<ChannelEntity> channelList = new List<ChannelEntity>();

        private string LauncherConfigPath;
        private string GameConfigPath;

        private string PCGameSDKPath;

        public ChannelHelper(string launcherPath, string gamePath)
        {
            LauncherConfigPath = launcherPath;
            GameConfigPath = gamePath;

            PCGameSDKPath = Path.GetDirectoryName(GameConfigPath) + @"\StarRail_Data\Plugins\PCGameSDK.dll";

            //File.Delete(GameConfigPath);
            //Trace.WriteLine("诡异文件(居然能在 \ 目录创建文件？？？)："+File.Exists(GameConfigPath));

            InitializeChannelList();
        }
        private void InitializeChannelList()
        {
            //官服国服-星穹列车
            AddChannel("[miHoYo] 星穹列车", 1, "gw_PC", 1);
            //B服-无名客
            AddChannel("[BILIBILI] 无名客", 14, "bilibili_PC", 0);
        }

        /// <summary>
        /// 添加服务器列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="channel"></param>
        /// <param name="cps"></param>
        /// <param name="sub_channel"></param>
        private void AddChannel(string name, int channel, string cps, int sub_channel)
        {
            ChannelEntity channelEnitiy = new ChannelEntity();
            channelEnitiy.ID = channelList.Count;
            channelEnitiy.Name = name;

            channelEnitiy.Channel = channel;
            channelEnitiy.CPS = cps;
            channelEnitiy.SubChannel = sub_channel;

            channelList.Add(channelEnitiy);
        }

        /// <summary>
        /// 获取列表里所有的 ChannelList Name
        /// </summary>
        /// <returns></returns>
        public string[] GetChannelNames()
        {
            List<string> channels = new List<string>();

            foreach (ChannelEntity channelEntity in channelList)
            {
                channels.Add(channelEntity.Name);
            }

            return channels.ToArray();
        }

        /// <summary>
        /// 获取当前选中的服务器
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            int index = -1;
            if (FileHelper.Exists(GameConfigPath))
            {
                index = GetChannel();
            }
            return index;
        }

        /// <summary>
        /// 通过GameConfig获取当前登录服务器
        /// </summary>
        /// <returns></returns>
        private int GetChannel()
        {
            IniFile iniFile = new IniFile(GameConfigPath);
            //写出启动器配置
            System.Diagnostics.Trace.WriteLine("GetChannel:" + GameConfigPath);
            System.Diagnostics.Trace.WriteLine(iniFile.ReadValue("General", "channel"));
            ChannelEntity server = channelList.Find(delegate (ChannelEntity s) { return s.Channel == Convert.ToInt32(iniFile.ReadValue("General", "channel")); });
            return server.ID;
        }

        /// <summary>
        /// 设置当前登录服务器
        /// </summary>
        /// <param name="index"></param>
        public void Set(int index)
        {
            if (FileHelper.Exists(GameConfigPath))
            {
                ChannelEntity server = channelList.Find(delegate (ChannelEntity s) { return s.ID == index; });

                if (server.Channel == 14)
                {
                    ExportNeedFiles(PCGameSDKPath);
                }
                if (server.Channel == 1)
                {
                    DeleteExportFiles(PCGameSDKPath);
                }

                SetChannel(server.Channel, server.CPS, server.SubChannel);
            }
            else
            {
                SnackbarHelper.ShowDanger("错误：游戏配置文件不存在", "前往 [本体设置] 重新设置启动器路径后再试。");
            }
        }

        /// <summary>
        /// 设置 Launcher 和 Game 的登录服务器
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="cps"></param>
        /// <param name="SubChannel"></param>
        private void SetChannel(int channel, string cps, int SubChannel)
        {
            if (FileHelper.Exists(LauncherConfigPath))
            {
                IniFile iniLauncher = new IniFile(LauncherConfigPath);
                //写出启动器配置
                iniLauncher.WriteValue("launcher", "channel", channel.ToString());
                iniLauncher.WriteValue("launcher", "cps", cps);
                iniLauncher.WriteValue("launcher", "sub_channel", SubChannel.ToString());
            }
            else
            {
                SnackbarHelper.ShowDanger("错误：启动器 Config.ini 文件丢失",  "无法修改启动器的 Config.ini 文件。");
            }

            if (FileHelper.Exists(GameConfigPath))
            {
                IniFile iniGame = new IniFile(GameConfigPath);
                //写出游戏配置
                iniGame.WriteValue("General", "channel", channel.ToString());
                iniGame.WriteValue("General", "cps", cps);
                iniGame.WriteValue("General", "sub_channel", SubChannel.ToString());
            }
            else
            {
                SnackbarHelper.ShowDanger("错误：游戏 Config.ini 文件丢失", "切换登录服务器失败，前往 [本体设置] 重新设置游戏路径。");
            }
        }

        /// <summary>
        /// 导出需要文件
        /// </summary>
        /// <param name="exportFilePath"></param>
        private void ExportNeedFiles(string exportFilePath)
        {
            if (FileHelper.Exists(exportFilePath))
            {
                return;
            }
            try
            {
                byte[] byDll = Properties.Resources.PCGameSDK;
                using (FileStream fs = new FileStream(exportFilePath, FileMode.Create))
                {
                    fs.Write(byDll, 0, byDll.Length);
                }
            }
            catch (Exception e)
            {
                SnackbarHelper.ShowDanger("导出所需文件失败！", "请尝试已管理员启动后再次修改。\r\n设置管理员运行方式：右键软件 -> 弹出菜单选择 [属性] -> 新窗口上方选择 [兼容性] -> [勾选] 以管理员身份运行此程序");
            }
        }

        /// <summary>
        /// 删除不需要的文件
        /// </summary>
        /// <param name="exportFilePath"></param>
        private void DeleteExportFiles(string exportFilePath)
        {
            if (!FileHelper.Exists(exportFilePath))
            {
                return;
            }

            if (File.Exists(exportFilePath))
            {
                File.Delete(exportFilePath);
            }
        }
    }
}
