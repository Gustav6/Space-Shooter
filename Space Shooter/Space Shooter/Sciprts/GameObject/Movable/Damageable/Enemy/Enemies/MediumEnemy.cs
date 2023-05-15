using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Space_Shooter
{
    public class MediumEnemy : Enemy
    {
        private float shootCooldown;
        private float amountOfAttacksPerSecond = 0.5f;
        public MediumEnemy(Vector2 startPostion)
        {
            // Variables for Update
            moveSpeed = 200;
            health = 100;
            position = startPostion;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.mediumEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 168, 192);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Update(GameTime gameTime)
        {
            //Shoot(_gameTime);
            base.Update(gameTime);
        }

        public void Shoot(GameTime gameTime)
        {
            if (shootCooldown <= 0)
            {
                Data.gameObjects.Add(new Projectile(new Vector2(position.X - 25, position.Y), new Vector2(-1, 0), rotation + MathHelper.ToRadians(-90), this));
                shootCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                shootCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
