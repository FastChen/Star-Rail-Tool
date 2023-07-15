using Newtonsoft.Json;
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
using Wpf.Ui.Controls.Interfaces;

namespace Star_Rail_Tool.Controls
{
    /// <summary>
    /// GraphicsSettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class GraphicsSettingsControl : UserControl
    {
        public GraphicsSettingsControl()
        {
            InitializeComponent();
            InitallzeControls();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Get();
        }

        private void InitallzeControls()
        {
            List<GraphicsSettingsEntity> shadowQuality = new List<GraphicsSettingsEntity>();
            shadowQuality.Add(new GraphicsSettingsEntity { ID = 0, Name = "关" });
            //shadowQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低"});
            shadowQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            shadowQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            shadowQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            ComboBox_ShadowQuality.ItemsSource = shadowQuality;

            List<GraphicsSettingsEntity> renderScale = new List<GraphicsSettingsEntity>();
            renderScale.Add(new GraphicsSettingsEntity { ID = 0, Name = "0.6" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 1, Name = "0.8" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 2, Name = "1.0" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 3, Name = "1.2" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 4, Name = "1.4" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 5, Name = "1.6" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 6, Name = "1.8" });
            renderScale.Add(new GraphicsSettingsEntity { ID = 7, Name = "2.0" });
            ComboBox_RenderScale.ItemsSource = renderScale;

            List<GraphicsSettingsEntity> reflectionQuality = new List<GraphicsSettingsEntity>();
            reflectionQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低" });
            reflectionQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            reflectionQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            reflectionQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            reflectionQuality.Add(new GraphicsSettingsEntity { ID = 5, Name = "非常高" });
            ComboBox_ReflectionQuality.ItemsSource = reflectionQuality;

            List<GraphicsSettingsEntity> characterQuality = new List<GraphicsSettingsEntity>();
            //characterQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低" });
            characterQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            characterQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            characterQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            //characterQuality.Add(new GraphicsSettingsEntity { ID = 5, Name = "非常高" });
            ComboBox_CharacterQuality.ItemsSource = characterQuality;


            List<GraphicsSettingsEntity> envDetailQuality = new List<GraphicsSettingsEntity>();
            envDetailQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低" });
            envDetailQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            envDetailQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            envDetailQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            envDetailQuality.Add(new GraphicsSettingsEntity { ID = 5, Name = "非常高" });
            ComboBox_EnvDetailQuality.ItemsSource = envDetailQuality;

            List<GraphicsSettingsEntity> bloomQuality = new List<GraphicsSettingsEntity>();
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 0, Name = "关" });
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低" });
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            bloomQuality.Add(new GraphicsSettingsEntity { ID = 5, Name = "非常高" });
            ComboBox_BloomQuality.ItemsSource = bloomQuality;

            List<GraphicsSettingsEntity> aAMode = new List<GraphicsSettingsEntity>();
            aAMode.Add(new GraphicsSettingsEntity { ID = 0, Name = "关" });
            aAMode.Add(new GraphicsSettingsEntity { ID = 1, Name = "TAA" });
            aAMode.Add(new GraphicsSettingsEntity { ID = 2, Name = "FXAA" });
            ComboBox_AAMode.ItemsSource = aAMode;

            List<GraphicsSettingsEntity> lightQuality = new List<GraphicsSettingsEntity>();
            lightQuality.Add(new GraphicsSettingsEntity { ID = 1, Name = "非常低" });
            lightQuality.Add(new GraphicsSettingsEntity { ID = 2, Name = "低" });
            lightQuality.Add(new GraphicsSettingsEntity { ID = 3, Name = "中" });
            lightQuality.Add(new GraphicsSettingsEntity { ID = 4, Name = "高" });
            lightQuality.Add(new GraphicsSettingsEntity { ID = 5, Name = "非常高" });
            ComboBox_LightQuality.ItemsSource = lightQuality;
        }

        public void Get()
        {
            string data = GameHelper.GetGraphicsSettingsModel();
            if (!string.IsNullOrEmpty(data))
            {
               var entity = JsonConvert.DeserializeObject<GraphicsSettingsModelEntity>(data);

                ToggleSwitch_VSync.IsChecked = Convert.ToBoolean(entity.EnableVSync);

                ComboBox_RenderScale.SelectedValue = entity.RenderScale.ToString("0.0");

                ComboBox_ShadowQuality.SelectedValue = entity.ShadowQuality;
                ComboBox_ReflectionQuality.SelectedValue = entity.ReflectionQuality;
                ComboBox_CharacterQuality.SelectedValue = entity.CharacterQuality;
                ComboBox_EnvDetailQuality.SelectedValue = entity.EnvDetailQuality;
                ComboBox_BloomQuality.SelectedValue = entity.BloomQuality;
                ComboBox_AAMode.SelectedValue = entity.AAMode;
                ComboBox_LightQuality.SelectedValue = entity.LightQuality;

                System.Diagnostics.Trace.WriteLine("GetGraphicsSettingsModel:" +JsonConvert.SerializeObject(entity, Formatting.Indented));
            }
        }

        public void Set()
        {
            DialogHelper dialog = new DialogHelper();
            dialog.LeftButtonVisibility = System.Windows.Visibility.Visible;
            dialog.LeftButtonClick += new RoutedEventHandler(DialogLeftButtonClick);
            dialog.LeftButtonText = "确定修改";
            dialog.RightButtonText = "取消";
            dialog.Title = "确定修改游戏图像设置？";
            dialog.Content = "如果修改失败，尝试以管理员模式启动工具 或 进入游戏图形设置，手动关闭 [垂直同步] 选项，然后关闭游戏后再试。";
            dialog.Show();
        }

        private void DialogLeftButtonClick(object sender, RoutedEventArgs e)
        {
            var dialogControl = (IDialogControl)sender;
            dialogControl.Hide();

            try
            {
                bool enableVSync = (bool)ToggleSwitch_VSync.IsChecked;

                double renderScale = Convert.ToDouble(ComboBox_RenderScale.SelectedValue.ToString());

                int shadowQuality = Convert.ToInt32(ComboBox_ShadowQuality.SelectedValue.ToString());
                int reflectionQuality = Convert.ToInt32(ComboBox_ReflectionQuality.SelectedValue.ToString());
                int characterQuality = Convert.ToInt32(ComboBox_CharacterQuality.SelectedValue.ToString());
                int envDetailQuality = Convert.ToInt32(ComboBox_EnvDetailQuality.SelectedValue.ToString());
                int bloomQuality = Convert.ToInt32(ComboBox_BloomQuality.SelectedValue.ToString());
                int aAMode = Convert.ToInt32(ComboBox_AAMode.SelectedValue.ToString());
                int lightQuality = Convert.ToInt32(ComboBox_LightQuality.SelectedValue.ToString());

                GameHelper.SetGraphicsSettingsModel(enableVSync, renderScale, shadowQuality, reflectionQuality, characterQuality, envDetailQuality, bloomQuality, aAMode, lightQuality);
            }
            catch(Exception ex)
            {
                SnackbarHelper.ShowDanger("应用图形设置错误", "错误原因：" + ex.Message);
            }
        }


        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            Set();
        }

    }
}
