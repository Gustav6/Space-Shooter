using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public abstract class Movable : GameObject
    {
        public Vector2 velocity;
        public float moveSpeed;

        public Movable()
        {
            layerDeapth = 1;
            color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);

            base.Update(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            position += velocity * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }
    }
}
