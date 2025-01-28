using System;
using System.Collections.Generic;
using System.Threading;

namespace TankGame
{
    public class GameMenu
    {
        public static void StartMenu()
        {
            Console.Clear();

            Console.WriteLine("CHOOSE THE OPTION");
            Console.WriteLine("1.Play");
            Console.WriteLine("2.Settings");
            Console.WriteLine("q.Exit");
            Console.WriteLine();


            var choice = Console.ReadKey(true).Key;

            switch (choice)
            {
                case ConsoleKey.D1:
                    StartGame();
                    break;
                case ConsoleKey.D2:
                    Settings();
                    break;
            }
        }

        private static void Settings()
        {
            Console.WriteLine("CHOOSE THE OPTION");
            Console.WriteLine("1.Difficulty level");
            Console.WriteLine("2.Obstacle count");
            Console.WriteLine("3.Map size");
            Console.WriteLine("4.Select tank");
            Console.WriteLine("<-.Back");
            Console.WriteLine();

            GameProp();

            var choice = Console.ReadKey(true).Key;

            switch (choice)
            {
                case ConsoleKey.D1:
                    SelectDifficulty();
                    break;
                case ConsoleKey.D2:
                    SelectObstacles();
                    break;
                case ConsoleKey.D3:
                    MapSize();
                    break;
                case ConsoleKey.D4:
                    SelectTank();
                    break;
                case ConsoleKey.LeftArrow:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Wrong choice!");
                    break;
            }
        }

        private static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("The game is starting...");
            Thread.Sleep(200);

            var mapSize = MapSize();
            GameMap map = new GameMap(mapSize.Width, mapSize.Height);

            PlayerTank playerTank = new PlayerTank("player1", 0);

            List<Enemy> enemies = new List<Enemy>
                {
                    new Enemy(3, 2, 1, true),
                    new Enemy(6, 8, 2, true)
                };

            List<Position> obstacles = new List<Position>();

            GameSettings settings = new GameSettings
            {
                MapSize = (10, 20)
            };

            GameEngine engine = new GameEngine(map, playerTank, enemies, obstacles, settings);
            engine.Start();
            engine.GameLoop();
        }

        private static void SelectDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Choose the difficulty level");
            Console.WriteLine("1.Easy");
            Console.WriteLine("2.Medium");
            Console.WriteLine("3.Hard");
            Console.WriteLine("q.Back");

            var choice = Console.ReadKey(true).Key;

            switch (choice)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Selected: easy level");
                    GameSettings.Difficulty = 1;
                    GameSettings.EnemyCount = 3;
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Selected: medium level");
                    GameSettings.Difficulty = 2;
                    GameSettings.EnemyCount = 3;
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Selected: hard level");
                    GameSettings.Difficulty = 3;
                    GameSettings.EnemyCount = 8;
                    break;
                case ConsoleKey.Q:
                    Back();
                    break;
                default:
                    Console.WriteLine("Wrong choice, try again!");
                    SelectDifficulty();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Returning to the menu...");
            Thread.Sleep(1000);
            StartMenu();
        }

        private static void SelectTank()
        {
            Console.Clear();
            Console.WriteLine("1.Basic");
            Console.WriteLine("2.Fast");
            Console.WriteLine("3.Strong");
            Console.WriteLine("q.Back");

            var choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Selected: basic tank");
                    Tank.Speed = 2;
                    Tank.Damage = 1;
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Selected: fast tank");
                    Tank.Speed = 3;
                    Tank.Damage = 0.5;
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Selected: strong tank");
                    Tank.Speed = 1;
                    Tank.Damage = 2;
                    break;
                case ConsoleKey.Q:
                    Back();
                    break;
                default:
                    Console.WriteLine("Wrong choice, try again");
                    SelectTank();
                    break;

            }

            Console.WriteLine();
            Console.WriteLine("Returning to the menu...");
            Thread.Sleep(1000);
            StartMenu();
        }

        private static void SelectObstacles()
        {
            Console.Clear();
            Console.WriteLine("Choose the amount of obstacles");
            Console.WriteLine("1.Five obstacles");
            Console.WriteLine("2.Ten obstacles");
            Console.WriteLine("3.Fifteen obstacles");
            Console.WriteLine("q.Back");

            var choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Selected: 5 obstacles");
                    GameSettings.ObstacleCount = 5;
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Selected: 10 obstacles");
                    GameSettings.ObstacleCount = 10;
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Selected: 15 obstacles");
                    GameSettings.ObstacleCount = 15;
                    break;
                case ConsoleKey.Q:
                    Back();
                    break;
                default:
                    Console.WriteLine("Wrong choice, try again");
                    SelectObstacles();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Returning to the menu...");
            Thread.Sleep(1000);
            StartMenu();
        }

        public static (int Width, int Height) MapSize()
        {
            Console.Clear();
            Console.WriteLine("Choose the size of the map:");
            Console.WriteLine("1. 20x10");
            Console.WriteLine("2. 30x40");
            Console.WriteLine("3. 35x45");

            while (true)
            {
                var choice = Console.ReadKey(true).Key;

                switch (choice)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("You selected: 20x10");
                        return (20, 10);

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("You selected: 30x40");
                        return (30, 40);

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3: 
                        Console.WriteLine("You selected: 35x45");
                        return (35, 45);

                    default:
                        Console.WriteLine("Invalid choice. Please press 1, 2, or 3.");
                        break;
                }
            }
        }


        public static void Back()
        {
            StartMenu();
        }

        public static void GameProp()
        {
            Console.WriteLine("Current settings:");

            Console.WriteLine("Difficulty - " + GameSettings.Difficulty);

            if (Tank.Speed == 2)
            {
                Console.WriteLine("Your tank - basic");
            }
            else if (Tank.Speed == 3)
            {
                Console.WriteLine("Your tank - fast");
            }
            else if (Tank.Speed == 1)
            {
                Console.WriteLine("Your tank - strong");
            }

            Console.WriteLine("Obstacle count - " + GameSettings.ObstacleCount);
        }
    }
}
