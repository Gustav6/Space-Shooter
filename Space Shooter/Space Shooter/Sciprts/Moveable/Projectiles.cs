using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;

namespace Space_Shooter.Sciprts.Moveable
{
    internal class Projectiles : Movable
    {
        public float damage = 25;

        public Projectiles(Vector2 startPostion, Vector2 _velocity, float _rotation)
        {
            moveSpeed = 750;
            position = startPostion;
            velocity = _velocity;
            isRemoved = false;

            texture = Data.projectileTexture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = Color.White;
            rotation = _rotation;
            scale = 0.3f;
            sourceRectangle = new Rectangle(50, 100, 75, 60);
            layerDeapth = 1;
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * scale), (int)(sourceRectangle.Height * scale));
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            Hit();
            base.Update(gameTime);
        }

        public void Hit()
        {
            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is Characters c)
                {
                    if (hitbox.Intersects(c.hitbox))
                    {
                        isRemoved = true;
                    }
                }
            }
        }

        public void BorderControll()
        {

        }
    }
}
