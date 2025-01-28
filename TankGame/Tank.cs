using System;
using System.ComponentModel;

namespace TankGame
{
    public abstract class Tank
    {
        public double X { get; set; }
        public double Y { get; set; }
        public char Symbol { get; set;}
        public static int Speed { get; set; } = 1;
        public static double Damage { get; set; } = 1;
        public static double Health { get; set; } = 3;
        public bool IsAlive { get; set; }
//        protected Position CurrentCollision { get; set; }

        protected Tank(double x, double y, char symbol, int speed, double damage, double health)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Speed = speed;
            Damage = damage;
            Health = health;
         //   CurrentCollision = currentCollision;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public Direction LastDirection { get; protected set; }

        public abstract void Move(ConsoleKey key);
        public abstract Projectile Shoot();
        public abstract void TakeDamage(int damage);
    }
}