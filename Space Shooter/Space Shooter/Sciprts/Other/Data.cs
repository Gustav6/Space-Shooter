using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public class Data
    {
        public static List<GameObject> gameObject = new List<GameObject>();
        public static Texture2D[] arrayOfTextures = new Texture2D[100]; 

        public static Random rng = new Random();

        public static Texture2D hitBoxTexture;

        public static int bufferWidth = 1920;
        public static int bufferHeight = 1080;
    }

    enum TextureType
    {
        smallEnemyTexture,
        mediumEnemyTexture,
        bigEnemyTexture,
        playerTexture,
        projectileTexture
    }
}
