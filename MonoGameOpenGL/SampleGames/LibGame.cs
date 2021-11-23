using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameEx;

namespace MonoGameOpenGL.SampleGames
{
    public class LibGame : Game, KeyboardPress
    {
        KeyboardState KeyboardPress.CurrentKeyState { get; set; }
        KeyboardState KeyboardPress.PreviousKeyState { get; set; }

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _position = new(300, 300);
        private Texture2D _sprite;

        public LibGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            _sprite = Creator.CreateTexture2D(_graphics.GraphicsDevice, 100, 100, Color.DarkBlue);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            this.SetKeyState();

            if(this.IsKeyDown(Keys.Left)) _position.X -= 10;
            if(this.IsKeyDown(Keys.Right)) _position.X += 10;
            if(this.IsKeyPress(Keys.A)) _position.Y -= 10;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_sprite, _position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
