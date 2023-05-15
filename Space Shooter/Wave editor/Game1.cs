using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
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
        public BinaryFormatter bf;
        public SpriteFont font;

        private SaveWaveFormation[] saveWave;
        private const string PATH = "wave.json";

        public int tileSize = 64;
        public int gameWidth = 30;
        public int gameHeight = 17;
        public int currentWave = 1;
        public int loadedWave;

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

            font = Content.Load<SpriteFont>("Font");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Space_Shooter.TextureManager.LoadTextures(Content, GraphicsDevice);

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

            if (Input.HasBeenPressed(Keys.F11))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            for (int i = 0; i < Space_Shooter.Data.gameObjects.Count; i++)
            {
                if (Space_Shooter.Data.gameObjects[i].isRemoved)
                {
                    Space_Shooter.Data.gameObjects.RemoveAt(i);
                }
            }

            for (int i = 0; i < Space_Shooter.Data.gameObjects.Count; i++)
            {
                Space_Shooter.Data.gameObjects[i].Update(gameTime);
            }

            for (int i = 0; i < Space_Shooter.Data.gameObjects.Count; i++)
            {
                for (int x = 0; x < gameWidth; x++)
                {
                    for (int y = 0; y < gameHeight; y++)
                    {
                        if (Space_Shooter.Data.gameObjects[i].hitbox.Intersects(new Rectangle(x * tileSize, y * tileSize, 64, 64)))
                        {
                            Data.tileMap[x, y].hasGameObject = true;
                        }
                    }
                }
            }

            //SaveFuction();

            SaveFuntionTWO();
            LoadFunction();

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


            if (0 <= mouseX && mouseX < gameWidth)
            {
                if (0 <= mouseY && mouseY < gameHeight)
                {
                    #region Place game objects at mouse postion
                    if (Input.MouseHasBeenPressed(mouseState.LeftButton, previousMouseState.LeftButton))
                    {
                        if (!Data.tileMap[mouseX, mouseY].hasGameObject)
                        {
                            switch (selectedGameObject)
                            {
                                case 1:
                                    Space_Shooter.Data.gameObjects.Add(new SmallEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2), new Vector2(0, 0)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                                case 2:
                                    Space_Shooter.Data.gameObjects.Add(new MediumEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                                case 3:
                                    Space_Shooter.Data.gameObjects.Add(new BigEnemy(new Vector2(mouseX * tileSize + tileSize / 2, mouseY * tileSize + tileSize / 2)));
                                    Data.tileMap[mouseX, mouseY].hasGameObject = true;
                                    break;
                            }

                            for (int i = 0; i < Space_Shooter.Data.gameObjects.Count; i++)
                            {
                                Space_Shooter.Data.gameObjects[i].Update(gameTime);
                            }
                        }
                    }

                    #endregion

                    #region Removes game object at mouse postion

                    if (Input.MouseHasBeenPressed(mouseState.RightButton, previousMouseState.RightButton))
                    {
                        foreach (Space_Shooter.GameObject gameObjects in Space_Shooter.Data.gameObjects)
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

        public void SaveData(SaveWaveFormation[] save)
        {
            string serializedText = JsonSerializer.Serialize<SaveWaveFormation[]>(save);
            Trace.WriteLine(serializedText);
            
            File.WriteAllText(PATH, serializedText);
        }
        
        /*
        public void SaveFuction()
        {
            if (Input.HasBeenPressed(Keys.W))
            {
                saveWave = new SaveWaveFormation()
                {
                    postionX = new float[Space_Shooter.Data.gameObjects.Count],
                    postionY = new float[Space_Shooter.Data.gameObjects.Count],
                    enemyType = new EnemyType[Space_Shooter.Data.gameObjects.Count],
                };


                for (int i = 0; i < Space_Shooter.Data.gameObjects.Count; i++)
                {
                    saveWave.postionX[i] = Space_Shooter.Data.gameObjects[i].position.X;
                    saveWave.postionY[i] = Space_Shooter.Data.gameObjects[i].position.Y;

                    switch (Space_Shooter.Data.gameObjects[i])
                    {
                        case BigEnemy:
                            saveWave.enemyType[i] = EnemyType.bigEnemy;
                            break;
                        case MediumEnemy:
                            saveWave.enemyType[i] = EnemyType.mediumEnemy;
                            break;
                        case SmallEnemy:
                            saveWave.enemyType[i] = EnemyType.smallEnemy;
                            break;
                    }
                }
                
                SaveData(saveWave);
            }
        }*/

        public void SaveFuntionTWO()
        {

            if (Input.HasBeenPressed(Keys.W))
            {
                for (int i = 0; i < 1; i++)
                {

                    saveWave = new SaveWaveFormation[]
                    {
                        new SaveWaveFormation()
                        {
                            postionX = new float[Space_Shooter.Data.gameObjects.Count],
                            postionY = new float[Space_Shooter.Data.gameObjects.Count],
                            enemyType = new EnemyType[Space_Shooter.Data.gameObjects.Count],
                        },

                        new SaveWaveFormation()
                        {
                            postionX = new float[Space_Shooter.Data.gameObjects.Count],
                            postionY = new float[Space_Shooter.Data.gameObjects.Count],
                            enemyType = new EnemyType[Space_Shooter.Data.gameObjects.Count],
                        }
                    };


                    for (int j = 0; j < Space_Shooter.Data.gameObjects.Count; j++)
                    {
                        saveWave[i].postionX[j] = Space_Shooter.Data.gameObjects[j].position.X;
                        saveWave[i].postionY[j] = Space_Shooter.Data.gameObjects[j].position.Y;

                        switch (Space_Shooter.Data.gameObjects[j])
                        {
                            case BigEnemy:
                                saveWave[i].enemyType[j] = EnemyType.bigEnemy;
                                break;
                            case MediumEnemy:
                                saveWave[i].enemyType[j] = EnemyType.mediumEnemy;
                                break;
                            case SmallEnemy:
                                saveWave[i].enemyType[j] = EnemyType.smallEnemy;
                                break;
                        }
                    }

                    SaveData(saveWave);
                }
            }
        }

        public void LoadFunction()
        {
            if (Input.HasBeenPressed(Keys.Left) || Input.HasBeenPressed(Keys.Right) && currentWave != loadedWave)
            {
                string wave;
                wave = File.ReadAllText("Content/wave.json");
                SaveWaveFormation[] saveWaveFormation = JsonSerializer.Deserialize<SaveWaveFormation[]>(wave);

                for (int i = 0; i < saveWaveFormation[currentWave].enemyType.Length; i++)
                {
                    Vector2 postion = new Vector2(saveWaveFormation[currentWave].postionX[i], saveWaveFormation[currentWave].postionY[i]);

                    switch (saveWaveFormation[currentWave].enemyType[i])
                    {
                        case EnemyType.bigEnemy:
                            Space_Shooter.Data.gameObjects.Add(new BigEnemy(postion));
                            break;
                        case EnemyType.mediumEnemy:
                            Space_Shooter.Data.gameObjects.Add(new MediumEnemy(postion));
                            break;
                        case EnemyType.smallEnemy:
                            Space_Shooter.Data.gameObjects.Add(new SmallEnemy(postion, new Vector2(0, 0)));
                            break;
                        default:
                            break;
                    }
                }
                loadedWave = currentWave;
            }

            if (Input.HasBeenPressed(Keys.Left) && currentWave > 0)
            {
                currentWave--;
            }
            else if (Input.HasBeenPressed(Keys.Right) && currentWave < 1)
            {
                currentWave++;
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

            foreach (GameObject _gameObjects in Space_Shooter.Data.gameObjects)
            {
                _gameObjects.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(font , currentWave.ToString(), new Vector2(50, 50), Color.LightGreen);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}