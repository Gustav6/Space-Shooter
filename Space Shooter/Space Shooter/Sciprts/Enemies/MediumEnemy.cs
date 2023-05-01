using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Enemies
{
    internal class MediumEnemy : Enemies
    {
        public MediumEnemy(Vector2 startPostion)
        {
            moveSpeed = 200;
            health = 50;
            postion = startPostion;
            velocity = Vector2.Zero;
            isRemoved = false;
            hitbox = new Rectangle(0, 0, 64, 64);

            texture = Data.enemyTextureList[1];
            color = Color.White;
            rotation = MathHelper.ToRadians(180);
            scale = 0.5f;
            sourceRectangle = new Rectangle(0, 0, 100, 128);
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
