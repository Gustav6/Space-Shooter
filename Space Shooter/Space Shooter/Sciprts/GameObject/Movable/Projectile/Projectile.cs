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
    internal class Projectile : Movable
    {
        public float damage = 25;
        public Damageable owner;

        public Projectile(Vector2 _startPostion, Vector2 _velocity, float _rotation, Damageable _owner)
        {
            // Variables for Update
            moveSpeed = 750;
            position = _startPostion;
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

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            Hit();
            BorderControll();
            base.Update(gameTime);
        }

        private void Hit()
        {
            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is Damageable _damageable)
                {
                    if (hitbox.Intersects(_damageable.hitbox) && _damageable != owner)
                    {
                        isRemoved = true;
                    }
                }
            }
        }

        private void BorderControll()
        {
            if (position.X >= 2000 || position.X <= -100)
            {
                isRemoved = true;
            }
        }
    }
}
