﻿using Microsoft.Xna.Framework.Graphics;
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

        public BigEnemy(Vector2 startPosition)
        {
            // Variables for Update
            moveSpeed = 800;
            health = 250;
            position = startPosition;

            // Variables for Draw
            texture = Data.arrayOfTextures[(int)TextureType.bigEnemyTexture];
            rotation = MathHelper.ToRadians(0);
            spriteScale = 0.7f;
            sourceRectangle = new Rectangle(0, 0, 136, 200);
            hitbox = new Rectangle(0, 0, (int)(sourceRectangle.Width * spriteScale), (int)(sourceRectangle.Height * spriteScale));
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
                attackCooldown = amountOfAttacksPerSecond;
            }
            else
            {
                attackCooldown -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
