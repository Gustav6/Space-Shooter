using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public abstract class Movable : GameObject
    {
        public Vector2 direction;
        public float moveSpeed;
        public float contactDamage;
        private float amountOfSecondsAsInvincible = 0.2f;
        private float invincibilityTimer;

        public Movable()
        {
            layerDepth = 1;
            color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            UpdateHitbox();
        }

        public void DealContactDamage(GameTime gameTime, float damage, GameObject gameObject)
        {
            if (invincibilityTimer <= 0 && Data.player.hitbox.Intersects(gameObject.hitbox))
            {
                Data.player.health -= damage;
                invincibilityTimer = amountOfSecondsAsInvincible;
            }
            else
            {
                invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Move(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            position += direction * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }
    }
}