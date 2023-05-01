using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Enemies
{
    internal class SmallEnemy : Enemies
    {
        public SmallEnemy(Vector2 startPostion)
        {
            moveSpeed = 200;
            health = 50;
            position = startPostion;
            velocity = Vector2.Zero;
            isRemoved = false;

            texture = Data.enemyTextureList[0];
            color = Color.White;
            rotation = MathHelper.ToRadians(0);
            scale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 132, 128);
            layerDeapth = 1;
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * scale), (int)(sourceRectangle.Height * scale));
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            IsDead(this);
            base.Update(gameTime);
        }
    }
}
