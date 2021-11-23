using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameOpenGL.SampleGames.SpaceShip.Extentions;
using System;

namespace MonoGameOpenGL.SampleGames
{
    public class Aiming : Game
    {
#pragma warning disable IDE0052 // 読み取られていないプライベート メンバーを削除
        private readonly GraphicsDeviceManager _graphics;
#pragma warning restore IDE0052 // 読み取られていないプライベート メンバーを削除
        private SpriteBatch _spriteBatch;

        private Texture2D _targetSprite;
        private Texture2D _crosshairsSprite;
        private Texture2D _backgroundSprite;
        private SpriteFont _gameFont;

        private Vector2 _targetPosition = new(300, 300);
        private const int _targetRadisu = 45;
        private readonly Random _random = new();

        private MouseState _mouseState;
        private bool _mouseRelesed = true;
        private int _score = 0;

        private double _time = 100;

        public Aiming()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _targetSprite = Content.Load<Texture2D>("target");
            _crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            _backgroundSprite = Content.Load<Texture2D>("sky");
            _gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_time > 0) {
                _time -= gameTime.ElapsedGameTime.TotalSeconds;
            } else {
                _time = 0;
            }

            _mouseState = Mouse.GetState();

            if(_mouseState is { LeftButton: ButtonState.Pressed } && _mouseRelesed && _time > 0) {
                var mouseTargetDist = Vector2.Distance(_targetPosition, _mouseState.Position.ToVector2());
                if(mouseTargetDist < _targetRadisu) {
                    _score++;
                    _targetPosition.X = _random.Next(_targetRadisu, Window.ClientBounds.Width - _targetRadisu);
                    _targetPosition.Y = _random.Next(_targetRadisu, Window.ClientBounds.Height - _targetRadisu);
                }
                _mouseRelesed = false;
            } else if(_mouseState is { LeftButton: ButtonState.Released }) {
                _mouseRelesed = true;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundSprite, Vector2.Zero, Color.White);
            if(_time > 0)
                _spriteBatch.Draw(_targetSprite, _targetPosition.Subtract(_targetRadisu), Color.White);

            _spriteBatch.DrawString(_gameFont, $"Score: {_score}", new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(_gameFont, $"Time: {_time:F2}", new Vector2(3, 30), Color.White);

            _spriteBatch.Draw(_crosshairsSprite, _mouseState.Position.ToVector2().Subtract(_crosshairsSprite.Width / 2), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
