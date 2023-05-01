using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Space_Shooter.Sciprts.Heritage;
using Space_Shooter.Sciprts.Moveable;

namespace Space_Shooter.Sciprts.Enemies
{
    abstract class Enemies : Characters
    {
        public float contactDamage = 15;

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObjects in Data.gameObjects)
            {
                if (gameObjects is Projectiles p)
                {
                    if (hitbox.Intersects(p.hitbox))
                    {
                        Damage(this, p.damage);
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
