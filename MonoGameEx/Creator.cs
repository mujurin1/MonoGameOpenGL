using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameEx
{
    public static class Creator
    {
        /// <summary>
        ///   サイズをと色を元に単色のテクスチャを生成します
        /// </summary>
        /// <param name="device"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Texture2D CreateTexture2D(GraphicsDevice device, int width, int height, Color color)
        {
            var texture = new Texture2D(device, width, height);
            var data = new Color[width * height];

            for(var i = 0; i < data.Length; i++)
                data[i] = color;

            texture.SetData(data);

            return texture;
        }

        /// <summary>
        ///   サイズと色を決定するアクションを元にテクスチャを生成します
        /// </summary>
        /// <param name="device"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="func">色を決定する関数</param>
        /// <returns></returns>
        public static Texture2D CreateTexture2D(GraphicsDevice device, int width, int height, Func<int, Color> func)
        {
            var texture = new Texture2D(device, width, height);
            var data = new Color[width * height];

            for(var i = 0; i < data.Length; i++)
                data[i] = func(i);

            texture.SetData(data);

            return texture;
        }
    }
}
