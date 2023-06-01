using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts.Other
{
    public static class Background
    {
        private static Texture2D texture = Data.arrayOfTextures[(int)TextureType.background];
        private static Vector2 backgroundPostion = new Vector2(0, 0);
        private static Vector2 origin = new Vector2(0, 0);
        private static Rectangle sourceRectangle = new Rectangle(0, 0, Data.bufferWidth, Data.bufferHeight);
        private static float scale = 1;

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float parallaxSpeed = -80f;
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Background.backgroundPostion = new Vector2((float)gameTime.TotalGameTime.TotalSeconds * parallaxSpeed, 0f);

            backgroundPostion.X = backgroundPostion.X % sourceRectangle.Width;
            spriteBatch.Draw(texture, backgroundPostion, sourceRectangle, Color.White, 0, origin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, backgroundPostion + new Vector2(sourceRectangle.Width, 0f), sourceRectangle, Color.White, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
