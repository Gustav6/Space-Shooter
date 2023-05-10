using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.GameObject;
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
            _graphics.PreferredBackBufferWidth = Data.bufferWidth;
            _graphics.PreferredBackBufferHeight = Data.bufferHeight;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureManager.LoadTextures(this, GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");

            base.Initialize();

            Data.gameObject.Add(new Player(new Vector2(200, 500)));
            Data.gameObject.Add(new MediumEnemy(new Vector2(900, 500)));
            Data.gameObject.Add(new BigEnemy(new Vector2(900, 800)));
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

            for (int i = 0; i < Data.gameObject.Count; i++)
            {
                Data.gameObject[i].Update(gameTime);
            }

            for (int i = 0; i < Data.gameObject.Count; i++)
            {
                if (Data.gameObject[i].isRemoved)
                {
                    Data.gameObject.RemoveAt(i);
                }
            }

            if (Input.HasBeenPressed(Keys.F2))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (GameObject gameObjects in Data.gameObject)
            {
                gameObjects.Draw(_spriteBatch);

                if (gameObjects is Player p)
                {
                    _spriteBatch.DrawString(font, p.health.ToString(), new Vector2(50, 50), Color.LightGreen);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}