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
    public class Player : Damageable
    {
        private float invincibilityTimer;
        private float shootCooldown;
        private float amountOfAttacksPerSecond = 0.15f;
        private float amountOfSecondsAsInvincible = 0.2f;

        public Player(Vector2 _startPostion)
        {
            // Variables for update
            health = 15000;
            moveSpeed = 350;
            position = _startPostion;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.playerTexture];
            engineTexture = Data.arrayOfTextures[(int)TextureType.playerEngine];
            sourceRectangleEngine = new Rectangle(0, 0, 60, 128);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.4f;
            sourceRectangle = new Rectangle(0, 0, 100, 132);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale * 0.7), (int)(sourceRectangle.Height * spriteScale * 0.7f));
        }

        public override void Update(GameTime gameTime)
        {
            TakeDamage(gameTime);
            Shoot(gameTime);
            CheckMovementInput();
            Move(gameTime);
            BorderControll();
            base.Update(gameTime);
        }

        private void CheckMovementInput()
        {
            #region xInput
            velocity.X = 0;

            if (Input.IsPressed(Keys.Right))
            {
                velocity.X += 1;
            }

            if (Input.IsPressed(Keys.Left))
            {
                velocity.X -= 1;
            }
            #endregion

            #region yInput
            velocity.Y = 0;

            if (Input.IsPressed(Keys.Up))
            {
                velocity.Y -= 1;
            }
            if (Input.IsPressed(Keys.Down))
            {
                velocity.Y += 1;
            }

            #endregion
        }

        public void BorderControll()
        {
            position.Y = Math.Clamp(position.Y, hitbox.Height , Data.bufferHeight - hitbox.Height);
            position.X = Math.Clamp(position.X, hitbox.Width , Data.bufferWidth / 2.5f - hitbox.Height);
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        private void Shoot(GameTime gameTime)
        {
            if (Input.IsPressed(Keys.Space) && shootCooldown <= 0)
            {
                Data.gameObjects.Add(new Projectile(new Vector2(position.X, position.Y - 20), new Vector2(1, 0), rotation + MathHelper.ToRadians(90), this));
                Data.gameObjects.Add(new Projectile(new Vector2(position.X, position.Y + 20), new Vector2(1, 0), rotation + MathHelper.ToRadians(90), this));
                shootCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                shootCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void TakeDamage(GameTime gameTime)
        {
            if (invincibilityTimer <= 0)
            {
                #region Checks collisions with projectiles and enemies
                foreach (GameObject gameObjects in Data.gameObjects)
                {
                    if (gameObjects is Enemy e)
                    {
                        if (hitbox.Intersects(e.hitbox))
                        {
                            Damage(e.contactDamage);
                            invincibilityTimer = amountOfSecondsAsInvincible;
                        }
                    }
                }
                #endregion
            }
            #region Reset timer
            else
            {
                invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if (velocity.X >= 0.1f)
            {
                _spriteBatch.Draw(engineTexture, enginePostion, sourceRectangleEngine, color, rotation, origin, spriteScale, SpriteEffects.None, layerDeapth);
            }
            base.Draw(_spriteBatch);
        }

    }
}
