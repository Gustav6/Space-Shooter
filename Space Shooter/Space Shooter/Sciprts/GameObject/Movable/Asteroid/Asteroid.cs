using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Space_Shooter
{
    internal class Asteroid : Movable
    {
        private float funnyRotation;
        public Asteroid(Vector2 startPosition, Vector2 startVelocity, float _moveSpeed)
        {
            // Variables for update
            moveSpeed = _moveSpeed;
            position = startPosition;
            velocity = startVelocity;
            contactDamage = 50;

            // Variables for Draw
            layerDepth = 0;
            texture = Data.arrayOfTextures[(int)TextureType.asteroid];
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = MathHelper.ToRadians(0);
            spriteScale = 1;
            sourceRectangle = new Rectangle(0, 0, 100, 132);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale * 0.5), (int)(sourceRectangle.Height * spriteScale * 0.3f));
        }

        public override void Update(GameTime gameTime)
        {
            rotation = MathHelper.ToRadians(funnyRotation);
            funnyRotation += 1.75f;
            dealContactDamage(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            base.Draw(spriteBatch, font);
        }
    }
}
