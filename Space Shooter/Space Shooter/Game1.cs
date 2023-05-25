﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace Space_Shooter
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteFont font;
        public MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Data.bufferWidth;
            _graphics.PreferredBackBufferHeight = Data.bufferHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureManager.LoadTextures(Content, GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");

            base.Initialize();

            Data.gameObjects.Add(new Player(new Vector2(200, 500)));
            WaveManager.SpawnWave();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Input.GetState();
            mouseState = Input.GetMouseState();

            WaveManager.Waves(gameTime);

            // Update loop for game objects
            for (int i = Data.gameObjects.Count - 1; i >= 0; i--)
            {
                Data.gameObjects[i].Update(gameTime);
            }

            if (Input.HasBeenPressed(Keys.F11))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            // Remove loop
            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i].isRemoved)
                {
                    Data.gameObjects.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (GameObject gameObjects in Data.gameObjects)
            {
                gameObjects.Draw(_spriteBatch, font);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}