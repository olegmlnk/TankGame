using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TankGame.PlayerTank;

namespace TankGame
{

    //DEVELOPING
    public class Enemy : Tank
    {
        public int Position { get; set; }

        Random random = new Random();

        public Enemy(int x, int y, int position, bool isAlive) : base(1, 1, 'O', 2, 1, 3)
        {
            X = x;
            Y = y;
            Position = position;
            IsAlive = isAlive;
        }
        public override void Move(ConsoleKey key)
        {

        }

        public override Projectile Shoot()
        {
            // Визначення початкових координат і напрямку
            double projectileX = this.X; // Передбачається, що у PlayerTank є X і Y
            double projectileY = this.Y;

            // Визначення початкової позиції снаряда залежно від напрямку танка
            switch (this.LastDirection) // Передбачається, що є CurrentDirection
            {
                case Direction.Up:
                    projectileY--; // Рух вгору
                    break;
                case Direction.Down:
                    projectileY++; // Рух вниз
                    break;
                case Direction.Left:
                    projectileX--; // Рух вліво
                    break;
                case Direction.Right:
                    projectileX++; // Рух вправо
                    break;
            }

            // Створення нового снаряда
            return new Projectile(projectileX, projectileY, this.LastDirection);
        }

        public override void TakeDamage(int damage)
        {
            if (!IsAlive) return;

            Health -= damage;

            if (Health <= 0)
            {
                Health = 0;
                IsAlive = false;
                Console.WriteLine("The tank is destroyed. You lost!");
            }
        }


    }
}
