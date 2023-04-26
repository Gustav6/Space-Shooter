using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Moveable
{
    internal class Player : Characters
    {
        public Player(Vector2 startPostion)
        {
            health = 100;
            moveSpeed = 300;
            postion = startPostion;
            velocity = Vector2.Zero;
            color = Color.White;
            isRemoved = false;
            hitbox = new Rectangle(0, 0, 64, 64);
            texture = Data.playerTexture;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            base.Update(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            if (Input.IsPressed(Keys.Right))
            {
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Input.IsPressed(Keys.Left))
            {
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                velocity.X = 0;
            }

            if (Input.IsPressed(Keys.Up))
            {
                velocity.Y = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Input.IsPressed(Keys.Down))
            {
                velocity.Y = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                velocity.Y = 0;
            }
        }
    }
}
