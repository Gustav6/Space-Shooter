using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts
{
    internal class Projectiles : Movable
    {
        private float damage = 25;

        public Projectiles()
        {
            speed = 15;
            postion = Vector2.Zero;
            velocity = Vector2.Zero;
            color = Color.White;
            isRemoved = false;
            hitbox = new Rectangle(0, 0, 16, 16);
            texture = Data.projectileTexture;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
