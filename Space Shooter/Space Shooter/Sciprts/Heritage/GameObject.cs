using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts.Heritage
{
    abstract class GameObject
    {
        public Vector2 postion;

        public Rectangle hitbox;
        public Color color;

        public Texture2D texture;

        public bool isRemoved;

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, postion, color);
        }
    }
}
