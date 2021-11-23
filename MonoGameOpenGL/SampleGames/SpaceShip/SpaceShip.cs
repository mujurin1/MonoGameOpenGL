using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static MonoGameOpenGL.SampleGames.SpaceShip.Extentions.Texture2DObject;

namespace MonoGameOpenGL.SampleGames.SpaceShip
{
    public class SpaceShip : Game
    {
        private readonly Random _random = new();

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _spaceSprite;
        private SpriteFont _gameFont;
        private SpriteFont _timerFont;

        private Ship _player;
        private readonly List<Asteroid> _asteroids = new();

        private Texture2DPrefab<Asteroid> _asteroidPrefab;
        private double asteroidTime = 2;


        public SpaceShip()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 2d);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Ship(new Vector2(100, 100), Content.Load<Texture2D>("ship"), true);

            //var astroidSprite = Content.Load<Texture2D>("asteroid");
            var ast = Content.Load<Texture2D>("asteroid");
            _asteroidPrefab =
                new Texture2DPrefab<Asteroid>(
                    ast,
                    new Vector2(ast.Width / 2, ast.Height / 2),
                    (position, sprite, revision) => new Asteroid(position, sprite, revision));

            _spaceSprite = Content.Load<Texture2D>("space");
            _gameFont = Content.Load<SpriteFont>("spaceFont");
            _timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            asteroidTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if(asteroidTime <= 0) {
                _asteroids.Add(
                    _asteroidPrefab.Create(new Vector2(Window.ClientBounds.Width + 100, _random.Next(0, Window.ClientBounds.Height))));
                asteroidTime = 2;
            }

            _player.Update(gameTime);
            foreach(var asteroid in _asteroids) asteroid.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceSprite, Vector2.Zero, Color.White);
            _player.Draw(_spriteBatch);
            foreach(var astroid in _asteroids) astroid.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
