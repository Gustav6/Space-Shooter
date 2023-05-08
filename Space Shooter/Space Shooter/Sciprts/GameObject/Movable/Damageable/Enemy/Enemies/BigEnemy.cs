using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    public class BigEnemy : Enemy
    {
        private float attackCooldown;
        private float amountOfAttacksPerSecond = 3;

        public BigEnemy(Vector2 _startPostion)
        {
            // Variables for Update
            moveSpeed = 200;
            health = 250;
            position = _startPostion;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.bigEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.5f;
            sourceRectangle = new Rectangle(0, 0, 136, 200);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime _gameTime)
        {
            Attack(_gameTime);
            base.Update(_gameTime);
        }

        public void Attack(GameTime _gameTime)
        {
            if (attackCooldown <= 0)
            {
                Data.gameObjects.Add(new SmallEnemy(new Vector2(position.X - 75, (position.Y + 75)), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(position.X - 150, position.Y), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(position.X, position.Y), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(position.X - 75, position.Y), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(position.X - 75, (position.Y - 75)), new Vector2(-1, 0)));
                attackCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                attackCooldown -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
