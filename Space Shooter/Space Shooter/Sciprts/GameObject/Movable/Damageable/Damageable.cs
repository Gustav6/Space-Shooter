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
        public Vector2 enginePostion;
        private float eningeAnimationTimer;

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
                enginePostion.X = position.X + texture.Width / 2 * spriteScale + 10;
            }

            if (damageable is Player)
            {
                enginePostion.X = position.X - texture.Width / 2 * spriteScale;
            }

            if (velocity.Y == 0)
            {
                if (enginePostion.Y <= position.Y && eningeAnimationTimer <= 0)
                {
                    enginePostion.Y = position.Y + 1;
                    eningeAnimationTimer = 0.1f;
                }
                else if (enginePostion.Y >= position.Y && eningeAnimationTimer <= 0)
                {
                    enginePostion.Y = position.Y - 1;
                    eningeAnimationTimer = 0.1f;
                }
                else
                {
                    eningeAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (velocity.Y != 0)
            {
                if (velocity.Y >= 0)
                {
                    enginePostion.Y = position.Y;
                }
                if (velocity.Y <= 0)
                {
                    enginePostion.Y = position.Y;
                }
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
    }
}
