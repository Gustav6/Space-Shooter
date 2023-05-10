using Space_Shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave_editor
{
    internal class Data
    {
        public static Tile[,] tileMap;
        public static List<Space_Shooter.GameObject> gameObjectSelect = new List<GameObject>();

        public static List<Space_Shooter.GameObject> savedObjects = new List<GameObject>();
    }
}
