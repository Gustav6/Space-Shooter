using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Space_Shooter;
using System;
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

        private BinaryFormatter bf;
        private Texture2D tileTexture;

        private SaveWaveFormation[] saveWave;
        private SaveWaveFormation[] saveWaveFormation;
        private const string PATH = "../../../../Space Shooter/Content/Waves/wave.json";

        private int currentWave = 0;
        private int tileSize = 64;
        private int gameWidth = 30;
        private int gameHeight = 17;
        private int selectedGameObject = 1;
        private int MaximumAmountOfEnemies = 3;
        private float timer;

        bool hasSaved = true;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Space_Shooter.Data.bufferWidth;
            _graphics.PreferredBackBufferHeight = Space_Shooter.Data.bufferHeight;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Data.tileMap = new Tile[gameWidth, gameHeight];

            TextureManager.LoadTextures(Content, GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Space_Shooter.TextureManager.LoadTextures(Content, GraphicsDevice);

            tileTexture = new Texture2D(GraphicsDevice, 1, 1);
            tileTexture.SetData<Color>(new Color[] { Color.White });

            LoadFunction();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Space_Shooter.Input.GetStateCall();

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

            Inputs(gameTime);
            PlaceGameObjects(gameTime);

            base.Update(gameTime);
        }

        public void PlaceGameObjects(GameTime gameTime)
        {
            int mouseX = Input.currentMouseState.X / tileSize;
            int mouseY = Input.currentMouseState.Y / tileSize;

            if (0 <= mouseX && mouseX < gameWidth)
            {
                if (0 <= mouseY && mouseY < gameHeight)
                {
                    #region Place game objects at mouse postion
                    if (Input.MouseHasBeenPressed(Input.currentMouseState.LeftButton, Input.previousMouseState.LeftButton))
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

                    if (Input.MouseHasBeenPressed(Input.currentMouseState.RightButton, Input.previousMouseState.RightButton))
                    {
                        foreach (Space_Shooter.GameObject gameObjects in Space_Shooter.Data.gameObjects)
                        {
                            if (gameObjects is Enemy e)
                            {
                                if (Data.tileMap[mouseX, mouseY].hasGameObject && e.hitbox.Intersects(new Rectangle((int)Input.currentMouseState.Position.ToVector2().X, (int)Input.currentMouseState.Position.ToVector2().Y, 0, 0)))
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

        public void Inputs(GameTime gameTime)
        {
            #region All Inputs

            if (Input.HasBeenPressed(Keys.Up))
            {
                selectedGameObject++;
                if (selectedGameObject > MaximumAmountOfEnemies)
                {
                    selectedGameObject = MaximumAmountOfEnemies;
                }
            }
            else if (Input.HasBeenPressed(Keys.Down))
            {
                selectedGameObject--;
                if (selectedGameObject <= 0)
                {
                    selectedGameObject = 1;
                }
            }

            if (Input.HasBeenPressed(Keys.Add))
            {
                Array.Resize(ref saveWaveFormation, saveWaveFormation.Length + 1);
                currentWave = saveWaveFormation.Length - 1;
                Space_Shooter.Data.gameObjects.Clear();
                SaveFunction();
            }

            if (Input.HasBeenPressed(Keys.Left) && currentWave > 0)
            {
                currentWave--;
                LoadFunction();
            }
            else if (Input.HasBeenPressed(Keys.Right) && currentWave < saveWaveFormation.Length - 1)
            {
                currentWave++;
                LoadFunction();
            }

            if (Input.HasBeenPressed(Keys.S))
            {
                SaveFunction();
                hasSaved = true;
                timer = 1.5f;
            }

            if (timer <= 0)
            {
                hasSaved = false;
            }
            else
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            #endregion
        }

        public void SaveData(SaveWaveFormation[] save)
        {
            string serializedText = JsonSerializer.Serialize<SaveWaveFormation[]>(save);
            Trace.WriteLine(serializedText);
            
            File.WriteAllText(PATH, serializedText);
        }

        public void SaveFunction()
        {
            
            saveWave = new SaveWaveFormation[saveWaveFormation.Length];
            for (int i = 0; i < saveWaveFormation.Length; i++)
            {
                if (i != currentWave)
                {
                    saveWave[i] = saveWaveFormation[i];
                }
                else
                {
                    saveWave[i] = new SaveWaveFormation()
                    {
                        positionX = new float[Space_Shooter.Data.gameObjects.Count],
                        positionY = new float[Space_Shooter.Data.gameObjects.Count],
                        enemyType = new EnemyType[Space_Shooter.Data.gameObjects.Count],
                    };

                    for (int j = 0; j < saveWave[i].enemyType.Length; j++)
                    {
                        saveWave[i].positionX[j] = Space_Shooter.Data.gameObjects[j].position.X;
                        saveWave[i].positionY[j] = Space_Shooter.Data.gameObjects[j].position.Y;

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
                }
            }

            SaveData(saveWave);
        }

        public void LoadFunction()
        {
            Space_Shooter.Data.gameObjects.Clear();
            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    Data.tileMap[x, y].hasGameObject = false;
                }
            }

            string wave;
            wave = File.ReadAllText("../../../../Space Shooter/Content/Waves/wave.json");
            saveWaveFormation = JsonSerializer.Deserialize<SaveWaveFormation[]>(wave);

            for (int i = 0; i < saveWaveFormation[currentWave].enemyType.Length; i++)
            {
                Vector2 position = new Vector2(saveWaveFormation[currentWave].positionX[i], saveWaveFormation[currentWave].positionY[i]);

                switch (saveWaveFormation[currentWave].enemyType[i])
                {
                    case EnemyType.bigEnemy:
                        Space_Shooter.Data.gameObjects.Add(new BigEnemy(position));
                        break;
                    case EnemyType.mediumEnemy:
                        Space_Shooter.Data.gameObjects.Add(new MediumEnemy(position));
                        break;
                    case EnemyType.smallEnemy:
                        Space_Shooter.Data.gameObjects.Add(new SmallEnemy(position, new Vector2(0, 0)));
                        break;
                    default:
                        break;
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

            foreach (GameObject gameObjects in Space_Shooter.Data.gameObjects)
            {
                gameObjects.Draw(_spriteBatch, Space_Shooter.TextureManager.font);
            }

            _spriteBatch.DrawString(Space_Shooter.TextureManager.font, "Current Wave: " + currentWave.ToString(), new Vector2(50, 50), Color.LightBlue);

            switch (selectedGameObject)
            {
                case 1:
                    _spriteBatch.Draw(Space_Shooter.Data.arrayOfTextures[(int)TextureType.smallEnemyTexture], new Vector2(50, 800), Color.White);
                    break;
                case 2:
                    _spriteBatch.Draw(Space_Shooter.Data.arrayOfTextures[(int)TextureType.mediumEnemyTexture], new Vector2(50, 800), Color.White);
                    break;
                case 3:
                    _spriteBatch.Draw(Space_Shooter.Data.arrayOfTextures[(int)TextureType.bigEnemyTexture], new Vector2(50, 800), Color.White);
                    break;
                default:
                    break;
            }

            if (hasSaved)
            {
                _spriteBatch.DrawString(Space_Shooter.TextureManager.font, "Saved Wave", new Vector2(50, 100), Color.LightGreen);
            }

            _spriteBatch.DrawString(Space_Shooter.TextureManager.font, "Save: S  ,  Add Wave: +  ,  Change Ship: Arrow  Up, Down", new Vector2(200, 50), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}