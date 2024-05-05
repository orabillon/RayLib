using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Texture2D = Raylib_CsLo.Texture;

namespace Serpent
{
    public class Game
    {
        int[,] map = new int[40,40];
        private int _tailleCase = 10;

        private void _Init()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = 0;
                }
            }
        }

        public Game()
        {
            _Init();
        }

        public void Load() 
        {
        }

        public void Update() 
        {
        }

        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Raylib.BLACK);

            for (int l = 0; l < map.GetLength(0); l++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    int x = c * _tailleCase;
                    int y = l * _tailleCase;

                    Raylib.DrawRectangle(x + 30 ,y + 30,_tailleCase - 1, _tailleCase - 1, Raylib.GRAY);
                }
            }

            Raylib.EndDrawing();
        }

        public void Unload() 
        {
            
        }
    }
}
