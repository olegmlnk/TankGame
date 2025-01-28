using System;
using System.Xml.Schema;

namespace TankGame
{
    //DEVELOPING
    public class PlayerTank : Tank
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public PlayerTank(string playerName, int score) : base(5, 5, 'O', 2, 1, 3)
        {
            PlayerName = playerName;
            Score = 0;

        }

        Position startPosition = new Position(5, 5);
        //PlayerTank playerTank = new PlayerTank("Player1", );

        public override void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.W) { Y -= Speed; LastDirection = Direction.Up; }
            else if (key == ConsoleKey.S) { Y += Speed; LastDirection = Direction.Down; }
            else if (key == ConsoleKey.A) { X -= Speed; LastDirection = Direction.Left; }
            else if (key == ConsoleKey.D) { X += Speed; LastDirection = Direction.Right; }
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
           if(!IsAlive) return;

           Health -= damage;

            if(Health <= 0)
            {
                Health = 0;
                IsAlive = false;
                Console.WriteLine("The tank is destroyed. You lost!");
            }
        }
    }
}