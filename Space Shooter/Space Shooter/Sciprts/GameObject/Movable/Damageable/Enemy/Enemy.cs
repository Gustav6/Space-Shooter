using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Space_Shooter
{
    public abstract class Enemy : Ship
    {
        private float shootCooldown;
        private float amountOfAttacksPerSecond = 0.5f;
        Rectangle inbounds = new Rectangle(-Data.bufferWidth / 2, 0, Data.bufferWidth * 2, Data.bufferHeight);

        public override void Update(GameTime gameTime)
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            InBounds();
            if (!Data.waveEditorIsRunning)
            {
                DealContactDamage(gameTime, contactDamage, this);
            }
            base.Update(gameTime);
        }

        public void InBounds()
        {
            if (!hitbox.Intersects(inbounds))
            {
                isRemoved = true;
            }
        }
        public void Shoot(GameTime gameTime)
        {
            if (shootCooldown <= 0 && Data.gameObjects.Contains(Data.player))
            {
                double randomYVelocityForProjectile = Data.rng.NextDouble(-0.02, 0.02);
                Vector2 aimPosition = new Vector2(-1, (float)randomYVelocityForProjectile);

                Data.gameObjects.Add(new Projectile(new Vector2(position.X - 25, position.Y), aimPosition, this, projectileMoveSpeed, projectileDamage));
                shootCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                shootCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
