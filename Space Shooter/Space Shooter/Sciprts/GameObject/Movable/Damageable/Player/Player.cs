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
    internal class Player : Damageable
    {
        private float invincibilityTimer;
        private float shootCooldown;
        private float amountOfAttacksPerSecond = 0.15f;
        private float amountOfSecondsAsInvincible = 0.2f;

        public Player(Vector2 _startPostion)
        {
            // Variables for update
            health = 150;
            moveSpeed = 350;
            position = _startPostion;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.playerTexture];
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.5f;
            sourceRectangle = new Rectangle(0, 0, 100, 132);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale * 0.7f), (int)(sourceRectangle.Height * spriteScale * 0.7f));
        }

        public override void Update(GameTime _gameTime)
        {
            TakeDamage(_gameTime);
            Shoot(_gameTime);
            Move();
            base.Update(_gameTime);
        }

        private void Move()
        {
            #region xInput
            if (Input.IsPressed(Keys.Right))
            {
                velocity.X += 1;
            }
            else if (Input.IsPressed(Keys.Left))
            {
                velocity.X -= 1;
            }
            else
            {
                velocity.X = 0;
            }
            #endregion

            #region yInput
            if (Input.IsPressed(Keys.Up))
            {
                velocity.Y -= 1;
            }
            else if (Input.IsPressed(Keys.Down))
            {
                velocity.Y += 1;
            }
            else
            {
                velocity.Y = 0;
            }
            #endregion
        }
        private void Shoot(GameTime _gameTime)
        {
            if (Input.IsPressed(Keys.Space) && shootCooldown <= 0)
            {
                Data.gameObjects.Add(new Projectile(new Vector2(position.X + 25, position.Y), new Vector2(1, 0), rotation + MathHelper.ToRadians(90), true));
                shootCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                shootCooldown -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void TakeDamage(GameTime _gameTime)
        {
            if (invincibilityTimer <= 0)
            {
                #region Checks collisions with projectiles and enemies
                foreach (GameObject _gameObjects in Data.gameObjects)
                {
                    if (_gameObjects is Projectile p)
                    {
                        if (hitbox.Intersects(p.hitbox) && !p.playerOwnsProjectile)
                        {
                            Damage(this, p.damage);
                            invincibilityTimer = amountOfSecondsAsInvincible;
                        }
                    }
                    else if (_gameObjects is Enemy e)
                    {
                        if (hitbox.Intersects(e.hitbox))
                        {
                            Damage(this, e.contactDamage);
                            invincibilityTimer = amountOfSecondsAsInvincible;
                        }
                    }
                }
                #endregion
            }
            #region Reset timer
            else
            {
                invincibilityTimer -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

    }
}
