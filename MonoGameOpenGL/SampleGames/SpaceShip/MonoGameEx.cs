using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameOpenGL.SampleGames.SpaceShip.Extentions
{
    static class MonoGameEx
    {
        /// <summary>
        ///   Vector2 から同じ値を引いた値を返します
        /// </summary>
        public static Vector2 Subtract(this Vector2 vector, float sub)
            => new(vector.X - sub, vector.Y - sub);

    }

    public abstract class Texture2DObject
    {
        private Vector2 _revision;
        private Vector2 _position;

        /// <summary>
        ///   <para>描画座標</para>
        ///   <para>Position Revision を変更すると同時に描画座標を計算しキャッシュする</para>
        /// </summary>
        protected Vector2 DrawPositionCache;

        /// <summary>
        ///   見た目
        /// </summary>
        public Texture2D Sprite { get; set; }

        /// <summary>
        ///   座標と描画位置の差
        /// </summary>
        public Vector2 Revision {
            get => _revision;
            set {
                _revision = value;
                DrawPositionCache = _position - _revision;
            }
        }

        /// <summary>
        ///   座標
        /// </summary>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                DrawPositionCache = _position - _revision;
            }
        }

        //public Texture2DObject(Vector2 position, Texture2D sprite, bool isRevisionCenter = false)
        //    : this(position, sprite, isRevisionCenter ? new Vector2(sprite.Bounds.Width / 2, sprite.Bounds.Height / 2) : Vector2.Zero) { }
        public Texture2DObject(Vector2 position, Texture2D sprite, Vector2 revision)
        {
            this._revision = revision;
            this.Position = position;
            this.Sprite = sprite;
        }

        /// <summary>
        ///   アップデート
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        ///   描画
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, DrawPositionCache, Color.White);
        }


        public class Texture2DPrefab<T> where T : Texture2DObject
        {
            private Vector2 _revision;

            /// <summary>
            ///   見た目
            /// </summary>
            public Texture2D Sprite { get; set; }

            /// <summary>
            ///   座標と描画位置の差
            /// </summary>
            public Vector2 Revision {
                get => _revision;
                set {
                    _revision = value;
                }
            }

            public Func<Vector2, Texture2D, Vector2, T> Creator { get; set; }


            //public Texture2DPrefab(Texture2D sprite, Func<, bool isRevisionCenter = false)
            //    : this(sprite, isRevisionCenter ? new Vector2(sprite.Bounds.Width / 2, sprite.Bounds.Height / 2) : Vector2.Zero) { }
            public Texture2DPrefab(Texture2D sprite, Vector2 revision, Func<Vector2, Texture2D, Vector2, T> creator)
            {
                this._revision = revision;
                this.Sprite = sprite;
                this.Creator = creator;
            }

            public T Create(Vector2 position)
                => Creator(position, Sprite, Revision);
        }
    }
}
