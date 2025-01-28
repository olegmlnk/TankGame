using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading;


namespace TankGame
{
    public class GameEngine
    {
        private GameMap _map;
        private PlayerTank _playerTank;
        private List<Enemy> _enemies;
        private List<Position> _obstacles;
        private GameSettings _settings;
        private List<Projectile> _activeProjectiles = new List<Projectile>();

        
        public GameEngine(GameMap map, PlayerTank playerTank, List<Enemy> enemies, List<Position> obstacles, GameSettings gameSettings)
        {
            _map = map;
            _playerTank = playerTank;
            _enemies = new List<Enemy>();
            _obstacles = new List<Position>();
            _settings = gameSettings;
        }

        public void Start()
        {
            PlaceObstacles(GameSettings.ObstacleCount);
            SpawnEnemies(GameSettings.EnemyCount);
            GameLoop();
        }

        //randomly place obstacle on the map
        private void PlaceObstacles(int count)
        {
           Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Position obstaclePosition;
                do
                {
                    int x = random.Next(0, _map.Width);
                    int y = random.Next(0, _map.Height);
                    obstaclePosition = new Position(x, y);
                } while (!_map.IsPositionEmpty(obstaclePosition));

                _obstacles.Add(obstaclePosition);
                _map.PlaceObject(obstaclePosition, '#');

            }
        }

        private void SpawnEnemies(int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Position enemyPosition;

                do
                {
                    int x = random.Next(0, _map.Width);
                    int y = random.Next(0, _map.Height);
                    enemyPosition = new Position(x, y);

                } while (!_map.IsPositionEmpty(enemyPosition) ||
                         (enemyPosition.X == _playerTank.X && enemyPosition.Y == _playerTank.Y));

                var enemy = new Enemy(enemyPosition.X, enemyPosition.Y, i, true);
                _enemies.Add(enemy);

                _map.PlaceObject(enemyPosition, 'E');
            }
        }


        public void GameLoop()
        {
            bool isRunning = true;

            while (isRunning)
            {
                RenderMap();
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                {
                    isRunning = false;
                    break;
                }

                HandlePlayerInput(key);
                UpdateGameState();
            }

            if (!_playerTank.IsAlive)
            {
                Console.WriteLine("Game Over! You lost.");
                isRunning = false;
            }
            else if (_enemies.All(e => !e.IsAlive))
            {
                Console.WriteLine("Congratulations! You won.");
                isRunning = false;
            }
        }

        private void HandlePlayerInput(ConsoleKey key)
        {
            Position newPosition = new Position((int)_playerTank.X, (int)_playerTank.Y);
            Position oldPosition = new Position((int) _playerTank.X, ( int) _playerTank.Y);
            if (newPosition.X < 0 || newPosition.X >= _map.Width ||
    newPosition.Y < 0 || newPosition.Y >= _map.Height)
            {
                return; 
            }


            if (key == ConsoleKey.W) newPosition.Y -= 1;
            else if (key == ConsoleKey.S) newPosition.Y += 1;
            else if (key == ConsoleKey.A) newPosition.X -= 1;
            else if (key == ConsoleKey.D) newPosition.X += 1;

            if (_map.IsPositionEmpty(newPosition))
            {
                _map.PlaceObject(oldPosition, '_');
                _playerTank.Move(key);
                _map.PlaceObject(new Position((int)_playerTank.X, (int)_playerTank.Y), _playerTank.Symbol);
            }

            if (key == ConsoleKey.Spacebar)
            {
                var projectile = _playerTank.Shoot();
                _activeProjectiles.Add(projectile);
                _map.PlaceObject(new Position((int)projectile.X, (int)projectile.Y), projectile.Symbol);
            }

        }

        private void UpdateGameState()
        {
            foreach (var enemy in _enemies)
            {
                Position newPosition = new Position((int)enemy.X, (int)enemy.Y + 1);

                if (_map.IsPositionEmpty(newPosition))
                {
                    _map.PlaceObject(new Position((int)enemy.X, (int)enemy.Y), '_');
                    enemy.Move((ConsoleKey)new Random().Next(37, 41)); // Рандомізований рух ворогів

                    _map.PlaceObject(new Position((int)enemy.X, (int)enemy.Y), enemy.Symbol);
                }
            }

            foreach (var enemy in _enemies.ToList())
            {
                foreach (var projectile in _activeProjectiles.ToList())
                {
                    if ((int)projectile.X == (int)enemy.X && (int)projectile.Y == (int)enemy.Y)
                    {
                        enemy.TakeDamage(1);
                        projectile.Deactivate();
                        _activeProjectiles.Remove(projectile);
                        if (!enemy.IsAlive)
                        {
                            _enemies.Remove(enemy);
                            _map.PlaceObject(new Position((int)enemy.X, (int)enemy.Y), '_'); // Очищаємо місце ворога
                        }
                    }
                }
            }


            foreach (var projectile in _activeProjectiles.ToList()) // Ітерація по копії списку
            {
                projectile.Move();
                if (!projectile.IsActive || !_map.IsPositionEmpty(new Position((int)projectile.X, (int)projectile.Y)))
                {
                    _map.PlaceObject(new Position((int)projectile.X, (int)projectile.Y), '_'); // Видаляємо снаряд із карти
                    _activeProjectiles.Remove(projectile); 
                    continue;
                }

                _map.PlaceObject(new Position((int)projectile.X, (int)projectile.Y), projectile.Symbol);
            }

            foreach (var enemy in _enemies.ToList())
            {
                foreach (var projectile in _activeProjectiles.ToList())
                {
                    if ((int)projectile.X == (int)enemy.X && (int)projectile.Y == (int)enemy.Y)
                    {
                        enemy.TakeDamage(1);
                        projectile.Deactivate();
                        _activeProjectiles.Remove(projectile);
                        if (!enemy.IsAlive)
                        {
                            _enemies.Remove(enemy);
                            _map.PlaceObject(new Position((int)enemy.X, (int)enemy.Y), '_');
                        }
                    }
                }
            }

            // Additional game state updates can go here (e.g., projectile movement, collision detection)
        }


        private void RenderMap()
        {
            Console.Clear();
            _map.Render();
        }

    }
}