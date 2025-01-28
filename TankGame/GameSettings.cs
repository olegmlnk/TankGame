using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    public class GameSettings
    {
        public static int Difficulty { get; set; } = 1;
        public static int EnemyCount { get; set; } = 3;
        public static int ObstacleCount { get; set; } = 5;
        public (int Width, int Height) MapSize {  get; set; }
    }
}
