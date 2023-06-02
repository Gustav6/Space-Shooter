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
        public bool isMoving;

        public SmallEnemy(Vector2 startPosition, Vector2 _velocity)
        {
            // Variables for Update
            moveSpeed = 1000;
            health = 100;
            position = startPosition;
            direction = _velocity;
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

        public override void Update(GameTime gameTime)
        {
            Attack();
            base.Update(gameTime);
        }

        public void Attack()
        {
            if (position.Y + 5 >= Data.player?.position.Y && position.Y - 5 <= Data.player?.position.Y)
            {
                isMoving = true;
            }
            if (isMoving)
            {
                rotation = MathF.Atan2(direction.X, -direction.Y) + MathF.PI / 2;

                if (position.X >= Data.player.position.X)
                {
                    direction += Vector2.Normalize(Data.player.position - position) * 0.05f;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (direction.X <= -0.1f)
            {
                spriteBatch.Draw(engineTexture, enginePosition, sourceRectangleEngine, color, rotation, origin, spriteScale, SpriteEffects.None, layerDepth);
            }
            base.Draw(spriteBatch);
        }
    }
}
