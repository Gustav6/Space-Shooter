using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Shooter
{
    public class SmallEnemy : Enemy
    {
        public SmallEnemy(Vector2 startPosition, Vector2 _velocity)
        {
            // Variables for Update
            moveSpeed = 800;
            health = 50;
            position = startPosition;
            velocity = _velocity;
            engineTexture = Data.arrayOfTextures[(int)TextureType.smallEnemyEngine];
            sourceRectangleEngine = new Rectangle(0, 0, 64, 132);
            contactDamage = 10;

            // Variables for Draw
            layerDepth = 1;
            texture = Data.arrayOfTextures[(int)TextureType.smallEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 132, 128);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (velocity.X <= -0.1f)
            {
                spriteBatch.Draw(engineTexture, enginePosition, sourceRectangleEngine, color, rotation, origin, spriteScale, SpriteEffects.None, layerDepth);
            }
            base.Draw(spriteBatch, font);
        }
    }
}
