using Microsoft.Xna.Framework;
using System;

namespace Space_Shooter
{
    public abstract class Enemy : Damageable
    {
        Rectangle inbounds = new Rectangle(0, 0, Data.bufferWidth, Data.bufferHeight);

        public float contactDamage = 20;

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in Data.gameObject)
            {
                if (gameObject is Projectile p)
                {
                    if (hitbox.Intersects(p.hitbox) && p.owner != this)
                    {
                        Damage(this, p.damage);
                    }
                }
            }

            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            InBounds(this);
            base.Update(gameTime);
        }

        public void InBounds(Enemy e)
        {

            if (!e.hitbox.Intersects(inbounds))
            {
                isRemoved = true;
            }
        }
    }
}
