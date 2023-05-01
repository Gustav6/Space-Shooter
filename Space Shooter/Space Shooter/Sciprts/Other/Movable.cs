using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts.Heritage
{
    abstract class Movable : GameObject
    {
        public Vector2 velocity;
        public float moveSpeed; 

        public override void Update(GameTime gameTime)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            position += velocity * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitbox.Location = position.ToPoint();
            base.Update(gameTime);
        }
    }
}
