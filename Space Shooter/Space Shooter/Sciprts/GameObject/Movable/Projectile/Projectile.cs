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
        public float damage = 25;
        private float angleOfRotation;
        public Damageable owner;

        public Projectile(Vector2 startPostion, Vector2 _velocity, float _rotation, Damageable _owner)
        {
            // Variables for Update
            moveSpeed = 1100;
            position = startPostion;
            velocity = _velocity;
            owner = _owner;

            // Variables for Draw
            rotation = _rotation;
            texture = Data.arrayOfTextures[(int)TextureType.projectileTexture];
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 36, 56);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Update(GameTime gameTime)
        {
            rotation = (float)Math.Atan2(velocity.X, -velocity.Y);

            Hit();
            Move(gameTime);
            BorderControll();
            base.Update(gameTime);
        }

        private void Hit()
        {
            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is Damageable d)
                {
                    if (hitbox.Intersects(d.hitbox) && d != owner)
                    {
                        if ((owner is Enemy && d is Player) || (owner is Player && d is Enemy))
                        {
                            d.Damage(damage);
                            isRemoved = true;
                        }
                    }
                }
            }
        }

        private void BorderControll()
        {
            if (position.X >= Data.bufferWidth + 100 || position.X <= 0 - 100)
            {
                isRemoved = true;
            }
        }
    }
}
