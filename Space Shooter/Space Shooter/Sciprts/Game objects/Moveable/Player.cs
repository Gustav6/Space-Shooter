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
            moveSpeed = 3;
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
        public override void Update()
        {
            base.Update();
        }

        public void MoveRight()
        {
            if (Input.IsPressed(Keys.Right))
            {
                velocity.X = moveSpeed;
            }
        }

        public void MoveLeft()
        {
            if (Input.IsPressed(Keys.Left))
            {
                velocity.X = -moveSpeed;
            }
        }
    }
}
