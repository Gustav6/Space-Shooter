using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace Space_Shooter
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Data.bufferWidth;
            graphics.PreferredBackBufferHeight = Data.bufferHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureManager.LoadTextures(Content, GraphicsDevice);

            base.Initialize();

            Data.gameObjects.Add(new Player(new Vector2(200, 500)));
            WaveManager.SpawnWave();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Input.GetStateCall();

            WaveManager.Waves(gameTime);

            // Update loop for game objects
            for (int i = Data.gameObjects.Count - 1; i >= 0; i--)
            {
                Data.gameObjects[i].Update(gameTime);
            }

            if (Input.HasBeenPressed(Keys.F11))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
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

            spriteBatch.Begin();

            foreach (GameObject gameObjects in Data.gameObjects)
            {
                gameObjects.Draw(spriteBatch, TextureManager.font);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}