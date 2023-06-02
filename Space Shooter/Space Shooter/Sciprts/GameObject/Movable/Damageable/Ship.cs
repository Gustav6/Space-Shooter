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
    public abstract class Ship : Movable
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
            if (health <= 0)
            {
                Destroy();
            }

            base.Update(gameTime);
            EngineAnimation(gameTime, this);
        }

        public void DealDamage(float damage)
        {
            health -= damage;
        }

        public void EngineAnimation(GameTime gameTime, Ship ship)
        {
            Vector2 offset = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));

            if (engineAnimationTimer <= 0)
            {
                offset *= 0.9f;
                engineAnimationTimer = 0.1f;
            }
            else
            {
                engineAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (ship is Enemy)
            {
                enginePosition.X = position.X + texture.Width / 2 * spriteScale + 10;
                enginePosition = position + offset * spriteScale * texture.Bounds.Size.ToVector2();
            }

            if (ship is Player)
            {
                enginePosition.X = position.X - texture.Width / 2 * spriteScale;
                enginePosition = position - offset * spriteScale * texture.Bounds.Size.ToVector2() * 0.7f;
            }
        }
    }
}
