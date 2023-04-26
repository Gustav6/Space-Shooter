using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts;
using Space_Shooter.Sciprts.Heritage;
using Space_Shooter.Sciprts.Moveable;

namespace Space_Shooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            //Data.enemyTexture = Content.Load<Texture2D>("");
            //Data.projectileTexture = Content.Load<Texture2D>("");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Data.gameObjects.Add(new Player(new Vector2(200, 200)));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.GetState();

            foreach (Movable m in Data.gameObjects)
            {
                m.Update();

                if (m is Player p)
                {
                    p.MoveRight();
                    p.MoveLeft();
                }

                if (m is Enemy e)
                {
                    e.Move();
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
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}