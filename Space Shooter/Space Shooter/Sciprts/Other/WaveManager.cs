using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Space_Shooter
{
    public class WaveManager
    {
        private static int currentWave = 0;
        private static float waveTimer = 60;
        private static SaveWaveFormation[] saveWaveFormations;
        private static Vector2 startPosition;
        private static float asteroidSpawnTimer;
        public static float asteroidSpeed = 600;

        public static void Waves(GameTime gameTime)
        {
            bool found = false;

            for (int i = 0; i < Data.gameObjects.Count; i++)
            {
                if (Data.gameObjects[i] is Enemy)
                {
                    found = true;
                }
            }

            if (!found && currentWave != saveWaveFormations.Length - 1)
            {
                currentWave++;
                waveTimer = 60;
                asteroidSpeed = 600 * (1 + 0.01f * currentWave);
                SpawnWave();
            }

            SpawnAsteroid(gameTime);
            CheckEnemyCount(gameTime);
        }

        public static void SpawnWave()
        {
            string wave;
            wave = File.ReadAllText("Content/Waves/wave.json");
            saveWaveFormations = JsonSerializer.Deserialize<SaveWaveFormation[]>(wave);

            for (int i = 0; i < saveWaveFormations[currentWave].enemyType.Length; i++)
            {
                startPosition = new Vector2(saveWaveFormations[currentWave].positionX[i], saveWaveFormations[currentWave].positionY[i]);

                switch (saveWaveFormations[currentWave].enemyType[i])
                {
                    case EnemyType.bigEnemy:
                        Data.gameObjects.Add(new BigEnemy(startPosition));
                        break;
                    case EnemyType.mediumEnemy:
                        Data.gameObjects.Add(new MediumEnemy(startPosition));
                        break;
                    case EnemyType.smallEnemy:
                        Data.gameObjects.Add(new SmallEnemy(startPosition, new Vector2(0, 0)));
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CheckEnemyCount(GameTime gameTime)
        {
            if (waveTimer <= 0)
            {
                for (int i = 0; i < Data.gameObjects.Count; i++)
                {
                    if (Data.gameObjects[i] is Enemy e && !e.isRemoved)
                    {
                        e.direction.X = -1;
                    }
                }
            }
            else
            {
                waveTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                Data.waveTimeLeft = (int)waveTimer;
            }
        }

        private static void SpawnAsteroid(GameTime gameTime)
        {
            if (asteroidSpawnTimer <= 0 && waveTimer >= 0)
            {
                float randomYPosition = Data.rng.Next(150, Data.bufferHeight - 150);
                Data.gameObjects.Add(new Asteroid(new Vector2(Data.bufferWidth * 1.2f, randomYPosition), new Vector2(-1, 0), asteroidSpeed));
                asteroidSpawnTimer = 3;
            }
            else
            {
                asteroidSpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
