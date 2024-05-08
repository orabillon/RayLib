using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Texture2D = Raylib_CsLo.Texture;

namespace Serpent
{
    enum Direction {
        left,up,right,down
    }

    enum eGameState
    {
        play,
        pause,
        gameOver
    }

    public class Game
    {
        int[,] map = new int[20,20];
        private int _tailleCase = 25;
        int mapWidth;
        int mapHeight;
        eGameState gameState;
        int offSetGame = 30;
        
        Queue<Point> snake = new Queue<Point>();
        Point head;
        int snakeLength;
        float snakeSpeed;
        Direction snakeDir;
        Direction NextDirection;

        Point Apple;

        float timer = 0;
        int score = 0;

        private void _Init()
        {
            mapWidth = map.GetLength(1);
            mapHeight = map.GetLength(0);

            for (int i = 0; i < mapHeight; i++)
            {
                for(int j = 0; j < mapWidth; j++)
                {
                    map[i,j] = 0;
                }
            }


            snake = new Queue<Point>();
            head = new Point(mapHeight/2, mapWidth/2);
            snake.Enqueue(head);

            timer = 0;
            score = 0;
            snakeSpeed = 0.5f;
            snakeDir = Direction.right;
            NextDirection = Direction.right;
            gameState = eGameState.pause;
            snakeLength = 3;

            NewApple();
        }

        private void SnakeMove(int pOffsetX, int pOffsetY)
        {
            Point newHead = new Point(head.X + pOffsetX, head.Y + pOffsetY);
           

            head = newHead;

            // GameOver ?? 
            // condition sortie ecran
            if(head.X < 0 || head.X > mapWidth - 1 || head.Y > mapHeight -1 || head.Y < 0) 
            {
                gameState = eGameState.gameOver;
            }
            // le serpent se mort
            if (isSnakeAt(head.Y, head.X))
            {
                gameState = eGameState.gameOver;
            }

            snake.Enqueue(newHead);

            if (snake.Count > snakeLength)
            {
                snake.Dequeue();
            }

        }

        private bool isSnakeAt(int pLigne, int pColonne)
        {
            bool isSnake = false;
            
            foreach (Point p in  snake)
            {
                if (p.X == pColonne && p.Y == pLigne) 
                { 
                    isSnake = true; 
                    break; 
                }
            }

            return isSnake;
        }
        private void NewApple() 
        {
            Random rnd = new Random();
            int c, l;
            do
            {
                c = rnd.Next(0, mapWidth);
                l = rnd.Next(0, mapHeight);

            } while (isSnakeAt(l,c));

            Apple = new Point(c, l);

            if(score%5 == 0)
            {
                snakeSpeed -= 0.05f; 
            }
        }

        private void Play() 
        {
            timer += Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && (snakeDir == Direction.up || snakeDir == Direction.down))
            { NextDirection = Direction.right; }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && (snakeDir == Direction.up || snakeDir == Direction.down))
            { NextDirection = Direction.left; }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) && (snakeDir == Direction.left || snakeDir == Direction.right))
            { NextDirection = Direction.up; }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && (snakeDir == Direction.left || snakeDir == Direction.right))
            { NextDirection = Direction.down; }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            { gameState = eGameState.pause; }

            if (timer >= snakeSpeed)
            {
                timer = 0;
                snakeDir = NextDirection;
                switch (snakeDir)
                {
                    case Direction.left:
                        SnakeMove(-1, 0);

                        break;
                    case Direction.up:
                        SnakeMove(0, -1);
                        break;
                    case Direction.right:
                        SnakeMove(1, 0);
                        break;
                    case Direction.down:
                        SnakeMove(0, 1);
                        break;
                    default:
                        break;
                }
            }
            // est ce que le serpent mange la pomme ?? 
            if (head == Apple) 
            {
                NewApple();
                snakeLength++;
                score++;
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
            switch (gameState)
            {
                case eGameState.play: 
                    Play();
                    break;
                case eGameState.pause:
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    { gameState = eGameState.play; }
                    break;
                case eGameState.gameOver:
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    { _Init(); }
                    break;
            }


        }

        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Raylib.WHITE);

            // Map
            for (int l = 0; l < mapHeight; l++)
            {
                for (int c = 0; c < mapWidth; c++)
                {
                    int x = c * _tailleCase;
                    int y = l * _tailleCase;

                    Raylib.DrawRectangleLines(x + offSetGame ,y + offSetGame,_tailleCase - 1, _tailleCase - 1, Raylib.GRAY);
                }
            }

            // Apple
            Raylib.DrawRectangle(Apple.X * _tailleCase + offSetGame, Apple.Y * _tailleCase + offSetGame, _tailleCase - 1, _tailleCase - 1, Raylib.RED);

            // Snake
            foreach (Point p in snake)
            {
                int x = p.X * _tailleCase;
                int y = p.Y * _tailleCase;

                Raylib.DrawRectangle(x + offSetGame, y + offSetGame, _tailleCase - 1, _tailleCase - 1, Raylib.DARKBLUE);
            }

            string direction = string.Empty;
            switch (snakeDir)
            {
                case Direction.left:
                    direction = "Gauche";
                    break;
                case Direction.up:
                    direction = "Haut";
                    break;
                case Direction.right:
                    direction = "Droite";
                    break;
                case Direction.down:
                    direction = "Bas";
                    break;
                default:
                    break;
            }

            string statusJeu = string.Empty;
            switch (gameState)
            {
                case eGameState.play: 
                    statusJeu = "Play";
                    break;
                case eGameState.pause:
                    statusJeu = "Pause";
                    break;
                case eGameState.gameOver:
                    statusJeu = "GameOver";
                    break;
            }


            Raylib.DrawText($"Direction : {direction}",offSetGame + mapHeight * _tailleCase + 10 , offSetGame,20, Raylib.DARKBLUE);
            Raylib.DrawText($"Etat du jeu : {statusJeu}",offSetGame + mapWidth * _tailleCase + 10, 30 + offSetGame,20, Raylib.DARKBLUE);
            Raylib.DrawText($"score : {score}",offSetGame + mapWidth * _tailleCase + 10, 60 + offSetGame,20, Raylib.DARKBLUE);


            if (gameState == eGameState.gameOver) 
            {
                Raylib.DrawText($"Game Over", 40, 200, 130, Raylib.VIOLET);  
            }

            Raylib.EndDrawing();
        }

        public void Unload() 
        {
            
        }
    }
}
