using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    abstract class Movable : GameObject
    {
        public Vector2 velocity;
        public float moveSpeed;

        public Movable()
        {
            isRemoved = false;
            velocity = new Vector2();

            layerDeapth = 1;
            color = Color.White;
        }

        public override void Update(GameTime _gameTime)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            position += velocity * moveSpeed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(_gameTime);
        }
    }
}
