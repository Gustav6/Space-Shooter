using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.Enemies;
using Space_Shooter.Sciprts.Heritage;
using Space_Shooter.Sciprts.Moveable;

namespace Space_Shooter.Sciprts.Player
{
    internal class Player : Characters
    {
        float invincibilityTimer;
        float shootCooldown;

        public Player(Vector2 startPostion)
        {
            health = 125;
            moveSpeed = 250;
            isRemoved = false;
            velocity = new Vector2();

            position = startPostion;
            color = Color.White;
            texture = Data.playerTexture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = MathHelper.ToRadians(180);
            scale = 0.3f;
            sourceRectangle = new Rectangle(0, 0, 162, 192);
            layerDeapth = 1;
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * scale * 0.7f), (int)(sourceRectangle.Height * scale * 0.7f));
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (invincibilityTimer <= 0)
            {
                foreach (GameObject gameObjects in Data.gameObjects)
                {
                    if (gameObjects is Projectiles p)
                    {
                        if (hitbox.Intersects(p.hitbox))
                        {
                            Damage(this, p.damage);
                            invincibilityTimer = 0.5f;
                        }
                    }
                    else if (gameObjects is Enemies.Enemies e)
                    {
                        if (hitbox.Intersects(e.hitbox))
                        {
                            Damage(this, e.contactDamage);
                            invincibilityTimer = 0.5f;
                        }
                    }
                }
            }
            else
            {
                invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Shoot(gameTime);
            IsDead(this);
            Move();
            base.Update(gameTime);
        }

        public void Move()
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
        public void Shoot(GameTime gametime)
        {
            if (Input.IsPressed(Keys.Space) && shootCooldown <= 0)
            {
                Data.gameObjects.Add(new Projectiles(new Vector2((position.X - origin.X * scale) + 150, (position.Y - origin.Y * scale)), new Vector2(1, 0), rotation + MathHelper.ToRadians(270)));
                shootCooldown = 0.2f;
            }
            else
            {
                shootCooldown -= (float)gametime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
