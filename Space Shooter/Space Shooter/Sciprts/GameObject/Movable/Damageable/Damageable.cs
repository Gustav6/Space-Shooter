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
    abstract class Damageable : Movable
    {
        public float health;

        public override void Update(GameTime _gameTime)
        {
            IsDead(this);
            base.Update(_gameTime);
        }

        public static void Damage(Damageable _damageable, float _damage)
        {
            _damageable.health -= _damage;
        }

        public void IsDead(Damageable _damageable)
        {
            if (health <= 0)
            {
                _damageable.isRemoved = true;
            }
        }
    }
}
