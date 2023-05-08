using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public class TextureManager
    {
        public static void LoadTextures(Game _game1, GraphicsDevice _graphicsDevice)
        {
            Data.hitBoxTexture = new Texture2D(_graphicsDevice, 1, 1);
            Data.hitBoxTexture.SetData<Color>(new Color[] { Color.Green * 0.3f });

            Data.arrayOfTextures.SetValue(_game1.Content.Load<Texture2D>("whiteenemy"), (int)TextureType.smallEnemyTexture);
            Data.arrayOfTextures.SetValue(_game1.Content.Load<Texture2D>("purpleenemy"), (int)TextureType.mediumEnemyTexture);
            Data.arrayOfTextures.SetValue(_game1.Content.Load<Texture2D>("orangeenemy"), (int)TextureType.bigEnemyTexture);
            Data.arrayOfTextures.SetValue(_game1.Content.Load<Texture2D>("Player"), (int)TextureType.playerTexture);
            Data.arrayOfTextures.SetValue(_game1.Content.Load<Texture2D>("Projectile"), (int)TextureType.projectileTexture);
        }
    }
}
