using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Shooter
{
    internal class SmallEnemy : Enemy
    {
        public SmallEnemy(Vector2 _startPostion, Vector2 _velocity)
        {
            // Variables for Update
            moveSpeed = 800;
            health = 50;
            position = _startPostion;
            velocity = _velocity;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.smallEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 132, 128);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
    }
}
