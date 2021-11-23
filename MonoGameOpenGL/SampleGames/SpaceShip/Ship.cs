using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameOpenGL.SampleGames.SpaceShip.Extentions;

namespace MonoGameOpenGL.SampleGames.SpaceShip
{
    class Ship : Texture2DObject
    {
        /// <summary>
        ///   １秒間に移動するピクセル数
        /// </summary>
        public int Speed { get; set; } = 180;

        public Ship(Vector2 position, Texture2D sprite, bool isRevisionCenter = false)
            : this(position, sprite, isRevisionCenter ? new Vector2(sprite.Bounds.Width / 2, sprite.Bounds.Height / 2) : Vector2.Zero) { }
        public Ship(Vector2 position, Texture2D sprite, Vector2 revision) : base(position, sprite, revision) { }

        public override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var moveVec2 = Vector2.Zero;

            float fSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;

            if(kState.IsKeyDown(Keys.Right)) moveVec2.X += fSpeed;
            if(kState.IsKeyDown(Keys.Left)) moveVec2.X -= fSpeed;
            if(kState.IsKeyDown(Keys.Up)) moveVec2.Y -= fSpeed;
            if(kState.IsKeyDown(Keys.Down)) moveVec2.Y += fSpeed;

            Position += moveVec2;
        }

    }
}
