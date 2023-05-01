using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts;
using Space_Shooter.Sciprts.Enemies;
using Space_Shooter.Sciprts.Gui;
using Space_Shooter.Sciprts.Heritage;
using Space_Shooter.Sciprts.Moveable;
using Space_Shooter.Sciprts.Player;

namespace Space_Shooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Data.playerTexture = Content.Load<Texture2D>("Player");
            Data.projectileTexture = Content.Load<Texture2D>("Projectile");
            Data.hitBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            Data.hitBoxTexture.SetData<Color>(new Color[] { Color.Green * 0.2f});

            // The list goes as follows white enemy is number 0, red enemy is number 1, and orange enemy is number 3.
            Data.enemyTextureList.Add(Content.Load<Texture2D>("whiteenemy"));
            Data.enemyTextureList.Add(Content.Load<Texture2D>("redenemy"));
            Data.enemyTextureList.Add(Content.Load<Texture2D>("orangeenemy"));
            font = Content.Load<SpriteFont>("Font");

            base.Initialize();

            Data.gameObjects.Add(new Player(new Vector2(500, 500)));
            Data.gameObjects.Add(new SmallEnemy(new Vector2(300, 500)));
            Data.gameObjects.Add(new MediumEnemy(new Vector2(900, 500)));
            Data.gameObjects.Add(new BigEnemy(new Vector2(1200, 500)));
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

            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                Data.gameObjects[i].Update(gameTime);
            }

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