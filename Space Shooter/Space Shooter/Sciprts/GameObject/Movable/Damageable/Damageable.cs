using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    public abstract class Damageable : Movable
    {
        public float health;
        public Rectangle sourceRectangleEngine;
        public Texture2D engineTexture;
        public Vector2 enginePosition;
        public float projectileMoveSpeed;
        public float projectileDamage;
        private float engineAnimationTimer;

        public override void Update(GameTime gameTime)
        {
            IsDead(this);
            EngineAnimation(gameTime, this);
            base.Update(gameTime);
        }

        public void Damage(float damage)
        {
            health -= damage;
        }

        public void IsDead(Damageable damageable)
        {
            if (health <= 0)
            {
                damageable.isRemoved = true;
            }
        }

        public void EngineAnimation(GameTime gameTime, Damageable damageable)
        {
            if (damageable is Enemy)
            {
                enginePosition.X = position.X + texture.Width / 2 * spriteScale + 10;
            }

            if (damageable is Player)
            {
                enginePosition.X = position.X - texture.Width / 2 * spriteScale;
            }

            if (velocity.Y == 0)
            {
                if (enginePosition.Y <= position.Y && engineAnimationTimer <= 0)
                {
                    enginePosition.Y = position.Y + 1;
                    engineAnimationTimer = 0.1f;
                }
                else if (enginePosition.Y >= position.Y && engineAnimationTimer <= 0)
                {
                    enginePosition.Y = position.Y - 1;
                    engineAnimationTimer = 0.1f;
                }
                else
                {
                    engineAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (velocity.Y != 0)
            {
                if (velocity.Y >= 0)
                {
                    enginePosition.Y = position.Y;
                }
                if (velocity.Y <= 0)
                {
                    enginePosition.Y = position.Y;
                }
            }
        }
    }
}
