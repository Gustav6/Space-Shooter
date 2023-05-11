using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    public abstract class Damageable : Movable
    {
        public float health;
        public Rectangle sourceRectangleEngine;
        public Texture2D engineTexture;

        public override void Update(GameTime gameTime)
        {
            IsDead(this);
            base.Update(gameTime);
        }

        public static void Damage(Damageable damageable, float damage)
        {
            damageable.health -= damage;
        }

        public void IsDead(Damageable damageable)
        {
            if (health <= 0)
            {
                damageable.isRemoved = true;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
    }
}
