using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts.Heritage
{
    internal class GameObject
    {
        public Vector2 velocity;
        public Vector2 postion;

        public Rectangle hitbox;
        public Color color;

        public Texture2D texture;
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, postion, color);
        }

        public virtual void Update()
        {

        }
    }
}
