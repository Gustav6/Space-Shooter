using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    public class BigEnemy : Enemy
    {
        public BigEnemy(Vector2 startPosition)
        {
            // Variables for Update
            moveSpeed = 800;
            health = 250;
            position = startPosition;
            contactDamage = 20;
            projectileMoveSpeed = 900;
            contactDamage = 15;

            // Variables for Draw
            layerDepth = 1;
            texture = Data.arrayOfTextures[(int)TextureType.bigEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.7f;
            sourceRectangle = new Rectangle(0, 0, 136, 200);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }


        public override void Update(GameTime gameTime)
        {
            Shield();
            Shoot(gameTime);
            base.Update(gameTime);
        }

        public void Shield()
        {
            bool found = false;

            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is SmallEnemy || Data.gameObjects[i] is MediumEnemy)
                {
                    found = true;
                }
            }

            if (found)
            {
                color = Color.Blue * 0.75f;
                health = 250;
            }
            else
            {
                color = Color.White;
            }
        }
    }
}
