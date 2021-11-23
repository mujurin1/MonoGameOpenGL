using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.SampleGames.SpaceShip.Extentions;

namespace MonoGameOpenGL.SampleGames.SpaceShip
{
    class Asteroid : Texture2DObject
    {
        /// <summary>
        ///   １秒間に移動するピクセル数
        /// </summary>
        public int Speed { get; set; } = 220;

        /// <summary>
        ///   半径
        /// </summary>
        public int Radisu { get; set; }

        public Asteroid(Vector2 position, Texture2D sprite, Vector2 revision) : base(position, sprite, revision)
        {
            this.Radisu = sprite.Bounds.Width;
        }

        public override void Update(GameTime gameTime)
        {
            float fSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            Position -= new Vector2(fSpeed, 0);
        }
    }
}
