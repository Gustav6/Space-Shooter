using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public abstract class GameObject
    {
        public float rotation;
        public float spriteScale;
        public float layerDeapth;

        public Vector2 position;
        public Vector2 origin;

        public Rectangle hitbox;
        public Rectangle sourceRectangle;

        public Color color;

        public Texture2D texture;

        public bool isRemoved;

        public virtual void Start()
        {
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        public virtual void Update(GameTime _gameTime)
        {
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, spriteScale, SpriteEffects.None, layerDeapth);
            //_spriteBatch.Draw(Data.hitBoxTexture, hitbox, color);
        }
    }
}
