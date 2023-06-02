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
        public float layerDepth;

        public Vector2 position;
        public Vector2 origin;

        public Rectangle hitbox;
        public Rectangle sourceRectangle;

        public Color color;

        public Texture2D texture;

        public bool isRemoved;

        public SpriteFont font;

        public virtual void UpdateHitbox()
        {
            hitbox.Location = (position - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        }

        public abstract void Update(GameTime gameTime);

        public void Destroy()
        {
            isRemoved = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!isRemoved)
            {
                spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, spriteScale, SpriteEffects.None, layerDepth);

                //Draw hitboxes
                //spriteBatch.Draw(Data.hitBoxTexture, hitbox, color);
            }
        }
    }
}
