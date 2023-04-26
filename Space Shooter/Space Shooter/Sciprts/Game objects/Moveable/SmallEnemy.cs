using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Moveable
{
    internal class SmallEnemy : Enemies
    {
        public SmallEnemy(Vector2 startPostion)
        {
            moveSpeed = 5;
            health = 50;
            postion = startPostion;
            velocity = Vector2.Zero;
            color = Color.White;
            isRemoved = false;
            hitbox = new Rectangle(0, 0, 64, 64);
            texture = Data.enemyTexture;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
