using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.Moveable;

namespace Space_Shooter.Sciprts.Heritage
{
    abstract class Characters : Movable
    {
        public float health;
        public bool hasBeenDamaged;

        public void Damage(Characters characters, float damage)
        {
            characters.health -= damage;
        }

        public void IsDead(Characters characters)
        {
            if (health <= 0)
            {
                characters.isRemoved = true;
            }
        }
    }
}
