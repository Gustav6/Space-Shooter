﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.GameObject;
using Space_Shooter.Sciprts.Other;
using System.Xml.Linq;

namespace Space_Shooter
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureManager.LoadTextures(this, GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");

            base.Initialize();

            Data.gameObjects.Add(new Player(new Vector2(200, 500)));
            Data.gameObjects.Add(new MediumEnemy(new Vector2(900, 500)));
            Data.gameObjects.Add(new BigEnemy(new Vector2(900, 800)));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.GetState();

            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                Data.gameObjects[i].Update(_gameTime);
            }

            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i].isRemoved)
                {
                    Data.gameObjects.RemoveAt(i);
                }
            }

            base.Update(_gameTime);
        }

        protected override void Draw(GameTime _gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (GameObject _gameObjects in Data.gameObjects)
            {
                _gameObjects.Draw(_spriteBatch);

                if (_gameObjects is Player p)
                {
                    _spriteBatch.DrawString(font, p.health.ToString(), new Vector2(50, 50), Color.LightGreen);
                }
            }

            _spriteBatch.End();

            base.Draw(_gameTime);
        }
    }
}