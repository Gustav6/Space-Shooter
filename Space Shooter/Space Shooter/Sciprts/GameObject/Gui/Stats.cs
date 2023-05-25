using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Shooter
{
    internal class Stats : GuiObject
    {
        public void WhatStats(GameObject gameObject, SpriteBatch spriteBatch)
        {
            if (gameObject is Player p)
            {
                spriteBatch.DrawString(font, p.health.ToString(), new Vector2(50, 50), Color.LightGreen);
                spriteBatch.DrawString(font, p.moveSpeed.ToString(), new Vector2(50, 75), Color.Green);
                spriteBatch.DrawString(font, p.bulletSpread.ToString(), new Vector2(50, 100), Color.Blue);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
        }
    }
}
