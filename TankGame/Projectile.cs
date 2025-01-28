using System;
using System.Runtime.InteropServices;

namespace TankGame
{
    public class Projectile
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public char Symbol { get; } = '*';
        public bool IsActive { get; private set; } = true;

        private readonly PlayerTank.Direction _direction;

        public Projectile(double x, double y, PlayerTank.Direction direction)
        {
            X = x;
            Y = y;
            _direction = direction;
        }

        public void Move()
        {
            if (!IsActive) return;

            switch (_direction)
            {
                case PlayerTank.Direction.Up:
                    Y--;
                    break;
                case PlayerTank.Direction.Down:
                    Y++;
                    break;
                case PlayerTank.Direction.Left:
                    X--;
                    break;
                case PlayerTank.Direction.Right:
                    X++;
                    break;
            }

            if (X < 0 || Y < 0 || X >= 20 || Y >= 10)
            {
                IsActive = false;
            }
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }

}