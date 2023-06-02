using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    public class Player : Ship
    {
        private float shootCooldown;
        private float amountOfAttacksPerSecond = 0.15f;
        public float bulletSpread;

        public Player(Vector2 startPosition)
        {
            // Variables for update
            health = 200;
            moveSpeed = 350;
            position = startPosition;
            projectileDamage = 20;
            projectileMoveSpeed = 1200;
            bulletSpread = 1;

            // Variables for Draw
            layerDepth = 0;
            texture = Data.arrayOfTextures[(int)TextureType.playerTexture];
            engineTexture = Data.arrayOfTextures[(int)TextureType.playerEngine];
            sourceRectangleEngine = new Rectangle(0, 0, 60, 128);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.4f;
            sourceRectangle = new Rectangle(0, 0, 100, 132);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale * 0.7), (int)(sourceRectangle.Height * spriteScale * 0.7f));
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        public override void Update(GameTime gameTime)
        {
            CheckMovementInput();
            Shoot(gameTime);
            
            base.Update(gameTime);
            BorderControl();
        }

        private void CheckMovementInput()
        {
            #region xInput
            direction.X = 0;

            if (Input.IsPressed(Keys.D))
            {
                direction.X += 1;
            }

            if (Input.IsPressed(Keys.A))
            {
                direction.X -= 1;
            }
            #endregion

            #region yInput
            direction.Y = 0;

            if (Input.IsPressed(Keys.W))
            {
                direction.Y -= 1;
            }
            if (Input.IsPressed(Keys.S))
            {
                direction.Y += 1;
            }

            #endregion
        }

        public void BorderControl()
        {
            position.Y = Math.Clamp(position.Y, hitbox.Height , Data.bufferHeight - hitbox.Height);
            position.X = Math.Clamp(position.X, hitbox.Width , Data.bufferWidth / 2.5f - hitbox.Height);
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        private void Shoot(GameTime gameTime)
        {
            if (Input.currentMouseState.LeftButton == ButtonState.Pressed && shootCooldown <= 0 && Input.currentMouseState.Position.X >= position.X)
            {
                Vector2 aimPosition = new Vector2(Input.currentMouseState.Position.X - position.X, Input.currentMouseState.Position.Y - position.Y + bulletSpread);

                Data.gameObjects.Add(new Projectile(new Vector2(position.X, position.Y - 20), aimPosition, this, projectileMoveSpeed, projectileDamage));
                Data.gameObjects.Add(new Projectile(new Vector2(position.X, position.Y + 20), aimPosition, this, projectileMoveSpeed, projectileDamage));
                shootCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                shootCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void DisplayStats(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, health.ToString(), new Vector2(50, 100), Color.LightGreen);
            spriteBatch.DrawString(TextureManager.font, moveSpeed.ToString(), new Vector2(50, 125), Color.Green);
            spriteBatch.DrawString(TextureManager.font, bulletSpread.ToString(), new Vector2(50, 150), Color.Blue);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (direction.X >= 0.1f)
            {
                spriteBatch.Draw(engineTexture, enginePosition, sourceRectangleEngine, color, rotation, origin, spriteScale, SpriteEffects.None, layerDepth);
            }

            DisplayStats(spriteBatch);
            base.Draw(spriteBatch);
        }

    }
}
