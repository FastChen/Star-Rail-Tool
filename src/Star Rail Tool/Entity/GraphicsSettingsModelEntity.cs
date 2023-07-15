using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Rail_Tool
{
    public class GraphicsSettingsModelEntity
    {
        /// <summary>
        /// FPS
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// 垂直同步
        /// </summary>
        public bool EnableVSync { get; set; }

        /// <summary>
        /// 渲染精度
        /// </summary>
        public double RenderScale { get; set; }

        /// <summary>
        /// 图像质量-不用管。
        /// </summary>
        public int ResolutionQuality { get; set; }

        /// <summary>
        /// 阴影质量
        /// </summary>
        public int ShadowQuality { get; set; }

        /// <summary>
        /// 光照质量
        /// </summary>
        public int LightQuality { get; set; }

        /// <summary>
        /// 角色质量
        /// </summary>
        public int CharacterQuality { get; set; }

        /// <summary>
        /// 场景细节
        /// </summary>
        public int EnvDetailQuality { get; set; }

        /// <summary>
        /// 反射质量
        /// </summary>
        public int ReflectionQuality { get; set; }

        /// <summary>
        /// 泛光效果
        /// </summary>
        public int BloomQuality { get; set; }

        /// <summary>
        /// 抗锯齿
        /// </summary>
        public int AAMode { get; set; }

    }
}
