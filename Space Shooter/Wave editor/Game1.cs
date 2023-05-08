using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter;
using System.Collections.Generic;
using System.Threading;
using Wave_editor;

namespace Wave_editor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D tileBaseTexture;

        public MouseState mouseState;
        public KeyboardState keyboardState;

        public int tileSize = 16;
        public int gameWidth = 120;
        public int gameHeight = 65;

        public int selectedGameObject = 1;

        public bool isPaused;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            Data.tileMap = new Tile[gameWidth, gameHeight];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Space_Shooter.TextureManager.LoadTextures(this, GraphicsDevice);

            tileBaseTexture = new Texture2D(GraphicsDevice, 1, 1);
            tileBaseTexture.SetData<Color>(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            Space_Shooter.Input.GetState();

            int mouseX = mouseState.X / tileSize;
            int mouseY = mouseState.Y / tileSize;

            if (Input.HasBeenPressed(Keys.Q))
            {
                selectedGameObject++;
                if (selectedGameObject >= 4)
                {
                    selectedGameObject = 1;
                }
            }

            if (0 <= mouseX && mouseX < gameWidth)
            {
                if (0 <= mouseY && mouseY < gameHeight)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Data.tileMap[mouseX, mouseY] = new Tile() { hasGameObject = true };

                        switch (selectedGameObject)
                        {
                            case 1:
                                Space_Shooter.Data.gameObjects.Add(new SmallEnemy(new Vector2(mouseX * tileSize, mouseY * tileSize), new Vector2(0, 0)));
                                break;
                            case 2:
                                Space_Shooter.Data.gameObjects.Add(new MediumEnemy(new Vector2(mouseX * tileSize, mouseY * tileSize)));
                                break;
                            case 3:
                                Space_Shooter.Data.gameObjects.Add(new BigEnemy(new Vector2(mouseX * tileSize, mouseY * tileSize)));
                                break;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (GameObject _gameObjects in Space_Shooter.Data.gameObjects)
            {
                _gameObjects.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}