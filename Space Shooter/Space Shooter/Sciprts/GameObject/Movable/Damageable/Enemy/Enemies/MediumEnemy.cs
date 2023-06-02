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
        public MediumEnemy(Vector2 startPosition)
        {
            // Variables for Update
            moveSpeed = 800;
            health = 150;
            position = startPosition;
            projectileDamage = 15;
            projectileMoveSpeed = 900;
            contactDamage = 15;

            // Variables for Draw
            layerDepth = 0;
            texture = Data.arrayOfTextures[(int)TextureType.mediumEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 168, 192);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Update(GameTime gameTime)
        {
            if (!Data.waveEditorIsRunning)
            {
                Shoot(gameTime);
            }
            base.Update(gameTime);
        }
    }
}
