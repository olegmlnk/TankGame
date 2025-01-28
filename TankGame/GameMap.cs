using System;
using System.ComponentModel;
using System.Linq;

namespace TankGame
{
    public class GameMap
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public char[,] MapData { get; private set; }
        public int Obstacles { get; set; }
        public GameMap(int width, int height)
        {
            Width = width;
            Height = height;
            MapData = new char[Height, Width];
            InitializeMap();
        }

        private void InitializeMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    MapData[i, j] = '_';
                }
            }
        }

        public void PlaceObject(Position position, char symbol)
        {
            if (position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height)
            {
                MapData[position.Y, position.X] = symbol;
            }
        }

        public void Render()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(MapData[i, j]);
                }
                Console.WriteLine();
            }
        }

        public bool IsPositionEmpty(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height && MapData[position.Y, position.X] == '_';
        }
    }
}