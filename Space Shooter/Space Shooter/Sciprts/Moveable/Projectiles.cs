using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Moveable
{
    internal class Projectiles : Movable
    {
        public float damage = 25;

        public Projectiles(Vector2 startPostion)
        {
            moveSpeed = 0;
            postion = startPostion;
            velocity = Vector2.Zero;
            isRemoved = false;

            texture = Data.projectileTexture;
            color = Color.White;
            rotation = MathHelper.ToRadians(0);
            scale = 0.3f;
            sourceRectangle = new Rectangle(50, 100, 75, 60);
            layerDeapth = 1;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
