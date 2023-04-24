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
    internal class Player : Characters
    {
        public Player(Vector2 startPostion)
        {
            health = 100;
            speed = 7;
            postion = startPostion;
            velocity = Vector2.Zero;
            color = Color.White;
            isRemoved = false;
            hitbox = new Rectangle(0, 0, 64, 64);
            texture = Data.playerTexture;
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
