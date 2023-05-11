using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Wave_editor;

namespace Wave_editor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MouseState mouseState;
        public MouseState previousMouseState;
        public KeyboardState keyboardState;

        public int tileSize = 64;
        public int gameWidth = 30;
        public int gameHeight = 17;

        public Texture2D tileTexture;

        public int selectedGameObject = 1;

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

            tileTexture = new Texture2D(GraphicsDevice, 1, 1);
            tileTexture.SetData<Color>(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            Space_Shooter.Input.GetState();

            for (int i = 0; i < Space_Shooter.Data.gameObject.Count; i++)
            {
                if (Space_Shooter.Data.gameObject[i].isRemoved)
                {
                    Space_Shooter.Data.gameObject.RemoveAt(i);
                }
            }

            for (int i = 0; i < Space_Shooter.Data.gameObject.Count; i++)
            {
                for (int x = 0; x < gameWidth; x++)
                {
                    for (int y = 0; y < gameHeight; y++)
                    {
                        if (Space_Shooter.Data.gameObject[i].hitbox.Intersects(new Rectangle(x * tileSize, y * tileSize, 64, 64)))
                        {
                            Data.tileMap[x, y].hasGameObject = true;
                        }
                    }
                }
            }

            if (Input.HasBeenPressed(Keys.W))
            {
                for (int i = 0; i < Space_Shooter.Data.gameObject.Count; i++)
                {
                    Data.savedObjects = Space_Shooter.Data.gameObject;
                }
            }

            PlaceGameObjects(gameTime);

            base.Update(gameTime);
        }

        public void PlaceGameObjects(GameTime gameTime)
        {
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

            #region Check mouse postion to place game objects

            if (0 <= mouseX && mouseX < gameWidth)
            {
                if (0 <= mouseY && mouseY < gameHeight)
                {
                    if (Input.MouseHasBeenPressed(mouseState.LeftButton, previousMouseState.LeftButton))
                    {
                        if (!Data.tileMap[mouseX, mouseY].hasGameObject)
                        {
                            switch (selectedGameObject)
                            {
                                case 1:
                                    Space_Shooter.Data.gameObject.Add(new SmallEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2), new Vector2(0, 0)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                                case 2:
                                    Space_Shooter.Data.gameObject.Add(new MediumEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                                case 3:
                                    Space_Shooter.Data.gameObject.Add(new BigEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                            }

                            for (int i = 0; i < Space_Shooter.Data.gameObject.Count; i++)
                            {
                                Space_Shooter.Data.gameObject[i].Update(gameTime);
                            }
                        }
                    }

                    #endregion

                    #region Removes game object at mouse

                    if (Input.MouseHasBeenPressed(mouseState.RightButton, previousMouseState.RightButton))
                    {
                        foreach (Space_Shooter.GameObject gameObjects in Space_Shooter.Data.gameObject)
                        {
                            if (gameObjects is Enemy e)
                            {
                                if (Data.tileMap[mouseX, mouseY].hasGameObject && e.hitbox.Intersects(new Rectangle((int)mouseState.Position.ToVector2().X, (int)mouseState.Position.ToVector2().Y, 0, 0)))
                                {
                                    e.isRemoved = true;
                                    for (int x = 0; x < gameWidth; x++)
                                    {
                                        for (int y = 0; y < gameHeight; y++)
                                        {
                                            Data.tileMap[x, y].hasGameObject = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    _spriteBatch.Draw(tileTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Data.tileMap[x, y].hasGameObject ? Color.White : Color.Black);
                }
            }

            foreach (GameObject _gameObjects in Space_Shooter.Data.gameObject)
            {
                _gameObjects.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}