using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Space_Shooter
{
    public abstract class Enemy : Damageable
    {
        Rectangle inbounds = new Rectangle(-Data.bufferWidth / 2, 0, Data.bufferWidth * 3, Data.bufferHeight);

        public float contactDamage = 20;

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);

            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            InBounds();
            base.Update(gameTime);
        }

        public void InBounds()
        {
            if (!hitbox.Intersects(inbounds))
            {
                isRemoved = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            base.Draw(spriteBatch, font);
        }
    }
}
