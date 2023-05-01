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
        float timer;

        public Player(Vector2 startPostion)
        {
            health = 125;
            moveSpeed = 250;
            isRemoved = false;
            velocity = new Vector2();
            hitbox = new Rectangle(0, 0, 64, 64);

            postion = startPostion;
            color = Color.White;
            texture = Data.playerTexture;
            rotation = MathHelper.ToRadians(180);
            scale = 0.4f;
            sourceRectangle = new Rectangle(0, 0, 162, 192);
            layerDeapth = 1;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (timer >= 20 && !hasBeenDamaged)
            {
                foreach (GameObject gameObjects in Data.gameObjects)
                {
                    if (gameObjects is Projectiles p)
                    {
                        if (hitbox.Intersects(p.hitbox))
                        {
                            Damage(this, p.damage);
                            hasBeenDamaged = true;
                            timer = 0;
                        }
                    }
                    else if (gameObjects is Enemies.Enemies e)
                    {
                        if (hitbox.Intersects(e.hitbox))
                        {
                            Damage(this, e.contactDamage);
                            hasBeenDamaged = true;
                            timer = 0;
                        }
                    }
                }
            }

            if (timer >= 20 && hasBeenDamaged)
            {
                hasBeenDamaged = false;
            }

            timer++;

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
    }
}
