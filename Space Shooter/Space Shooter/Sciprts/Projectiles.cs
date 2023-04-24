using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts
{
    internal class Projectiles : GameObject
    {
        public Projectiles()
        {
            postion = Vector2.Zero;
            velocity = Vector2.Zero;
            color = Color.White;
            hitbox = new Rectangle(0, 0, 16, 16);
        }
    }
}
