using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    internal class Data
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static Texture2D[] arrayOfTextures = new Texture2D[100]; 

        public static Random rng = new Random();

        public static Texture2D hitBoxTexture;
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
