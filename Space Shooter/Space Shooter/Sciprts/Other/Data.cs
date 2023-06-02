using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Space_Shooter
{
    public static class Data
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static Texture2D[] arrayOfTextures = new Texture2D[100]; 

        public static Random rng = new Random();

        public static Texture2D hitBoxTexture;

        public static int bufferWidth = 1920;
        public static int bufferHeight = 1080;

        public static int waveTimeLeft;

        public static Player player;
        public static Asteroid asteroid;

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

    public enum TextureType
    {
        smallEnemyTexture,
        mediumEnemyTexture,
        bigEnemyTexture,
        playerTexture,
        projectileTexture,
        playerEngine,
        smallEnemyEngine,
        asteroid,
        background
    }

    public enum EnemyType
    {
        bigEnemy,
        mediumEnemy,
        smallEnemy
    }
}
