using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Space_Shooter
{
    public class WaveManager
    {
        private static int currentWave = 0;
        private static float waveTimer = 10;
        private static SaveWaveFormation[] saveWaveFormations;
        private static Vector2 startPosition;
        private static float smallEnemyAttack;

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
                waveTimer = 10;
                SpawnWave();
            }

            MoveWaveToAttack();
            SmallEnemyAttack(gameTime);
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

        private static void MoveWaveToAttack()
        {

        }

        private static void CheckEnemyCount(GameTime gameTime)
        {
            if (waveTimer <= 0)
            {
                for (int i = 0; i < Data.gameObjects.Count; i++)
                {
                    if (Data.gameObjects[i] is Enemy e && !e.isRemoved)
                    {
                        e.velocity.X -= 1;
                    }
                }
            }
            else
            {
                waveTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private static void SmallEnemyAttack(GameTime gameTime)
        {
            if (smallEnemyAttack <= 0 && waveTimer >= 0)
            {
                float randomYPosition = Data.rng.Next(150, Data.bufferHeight - 150);
                Data.gameObjects.Add(new SmallEnemy(new Vector2(Data.bufferWidth * 1.2f - 75, (randomYPosition + 75)), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(Data.bufferWidth * 1.2f - 150, randomYPosition), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(Data.bufferWidth * 1.2f, randomYPosition), new Vector2(-1, 0)));
                Data.gameObjects.Add(new SmallEnemy(new Vector2(Data.bufferWidth * 1.2f - 75, (randomYPosition - 75)), new Vector2(-1, 0)));
                smallEnemyAttack = 3;
            }
            else
            {
                smallEnemyAttack -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
