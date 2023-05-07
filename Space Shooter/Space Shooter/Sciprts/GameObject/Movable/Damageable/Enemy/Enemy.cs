using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Space_Shooter.Sciprts.GameObject;

namespace Space_Shooter
{
    abstract class Enemy : Damageable
    {
        public float contactDamage = 20;

        public override void Update(GameTime _gameTime)
        {
            foreach (GameObject _gameObjects in Data.gameObjects)
            {
                if (_gameObjects is Projectile p)
                {
                    if (hitbox.Intersects(p.hitbox) && p.owner != this)
                    {
                        Damage(this, p.damage);
                    }
                }
            }

            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            base.Update(_gameTime);
        }
    }
}
