using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace Space_Shooter
{
    public class TextureManager
    {
        public static void LoadTextures(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Data.hitBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            Data.hitBoxTexture.SetData<Color>(new Color[] { Color.Green * 0.3f });

            Data.arrayOfTextures[(int)TextureType.smallEnemyTexture] =  (contentManager.Load<Texture2D>("Enemy/whiteenemy"));
            Data.arrayOfTextures[(int)TextureType.mediumEnemyTexture] = (contentManager.Load<Texture2D>("Enemy/purpleenemy"));
            Data.arrayOfTextures[(int)TextureType.bigEnemyTexture] = (contentManager.Load<Texture2D>("Enemy/orangeenemy"));
            Data.arrayOfTextures[(int)TextureType.playerTexture] = (contentManager.Load<Texture2D>("Player/Player"));
            Data.arrayOfTextures[(int)TextureType.projectileTexture] = (contentManager.Load<Texture2D>("Projectile"));
            Data.arrayOfTextures[(int)TextureType.playerEngine] = (contentManager.Load<Texture2D>("Player/Player_Engine"));
            Data.arrayOfTextures[(int)TextureType.smallEnemyEngine] = (contentManager.Load<Texture2D>("whiteenemy_Engine"));
        }
    }
}
