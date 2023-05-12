using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Shooter.Sciprts.GameObject;
using System.Xml.Linq;
using System.IO;
using System.Text.Json;

namespace Space_Shooter
{
    public class WaveManager
    {
        public static void Wave1()
        {
            string wave1;
            wave1 = File.ReadAllText("Content/Waves/save.json");
            SaveWaveFormation saveWaveFormation = JsonSerializer.Deserialize<SaveWaveFormation>(wave1);

            for (int i = 0; i < saveWaveFormation.enemyType.Length; i++)
            {
                Vector2 postion = new Vector2(saveWaveFormation.postionX[i], saveWaveFormation.postionY[i]);

                switch (saveWaveFormation.enemyType[i])
                {
                    case EnemyType.bigEnemy:
                        Data.gameObjects.Add(new BigEnemy(postion));
                        break;
                    case EnemyType.mediumEnemy:
                        Data.gameObjects.Add(new MediumEnemy(postion));
                        break;
                    case EnemyType.smallEnemy:
                        Data.gameObjects.Add(new SmallEnemy(postion, new Vector2(-1, 0)));
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Wave2()
        {

        }
    }
}
