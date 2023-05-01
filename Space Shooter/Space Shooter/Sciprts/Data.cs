using Microsoft.Xna.Framework.Graphics;
using Space_Shooter.Sciprts.Heritage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts
{
    internal class Data
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<Texture2D> enemyTextureList = new List<Texture2D>();

        public static Random rng = new Random();

        public static Texture2D playerTexture;
        public static Texture2D projectileTexture;
    }
}
