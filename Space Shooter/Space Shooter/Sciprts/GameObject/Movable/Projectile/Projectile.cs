using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Shooter
{
    public class Projectile : Movable
    {
        public float damage;
        public Ship owner;

        public Projectile(Vector2 startPosition, Vector2 projectileVelocity, Ship ownerOfProjectile, float projectileMoveSpeed, float projectileDamage)
        {
            // Variables for Update
            moveSpeed = projectileMoveSpeed;
            position = startPosition;
            direction = projectileVelocity;
            owner = ownerOfProjectile;
            damage = projectileDamage;

            // Variables for Draw
            rotation = 0;
            texture = Data.arrayOfTextures[(int)TextureType.projectileTexture];
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 36, 56);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Update(GameTime gameTime)
        {
            rotation = MathF.Atan2(direction.X, -direction.Y);

            Hit();
            BorderControl();
            base.Update(gameTime);
        }

        private void Hit()
        {
            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is Ship s)
                {
                    if (hitbox.Intersects(s.hitbox) && s != owner)
                    {
                        if ((owner is Enemy && s is Player) || (owner is Player && s is Enemy))
                        {
                            s.DealDamage(damage);
                            isRemoved = true;
                        }

                        if (owner is SmallEnemy e && s is Player)
                        {
                            e.isMoving = true;
                        }
                    }
                }
                else if (Data.gameObjects[i] is Asteroid a)
                {
                    if (hitbox.Intersects(a.hitbox))
                    {
                        isRemoved = true;
                    }
                }
            }
        }

        private void BorderControl()
        {
            if (position.X >= Data.bufferWidth + 100 || position.X <= 0 - 100)
            {
                isRemoved = true;
            }
            else if (position.Y >= Data.bufferHeight + 100 || position.Y <= 0 - 100)
            {
                isRemoved = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
