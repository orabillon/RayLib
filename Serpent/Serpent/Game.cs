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
    public enum Direction {
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
        int[,] map = new int[25,25];
        private int _tailleCase = 20;
        int mapWidth;
        int mapHeight;
        eGameState gameState;
        int marginWidth;
        int marginHeight;


        Queue<SPoint> snake = new Queue<SPoint>();
        SPoint head;
        int snakeLength;
        float snakeSpeed;
        Direction snakeDir;
        Direction NextDirection;

        Point Apple;

        float timer = 0;
        int score = 0;

        Texture2D texApple;
        TextureRL[] texSnakeHead;
        TextureRL[] texSnakeTail;
        TextureRL[] texSnakeBody;

        Sound sndPomme;
        Music msPlay;

        private void _Init()
        {
            mapWidth = map.GetLength(1);
            mapHeight = map.GetLength(0);

            marginWidth = (Raylib.GetScreenWidth() - mapWidth * _tailleCase)/2;
            marginHeight = 30;

            for (int i = 0; i < mapHeight; i++)
            {
                for(int j = 0; j < mapWidth; j++)
                {
                    map[i,j] = 0;
                }
            }

            timer = 0;
            score = 0;
            snakeSpeed = 0.5f;
            snakeDir = Direction.right;
            NextDirection = Direction.right;
            gameState = eGameState.pause;
            snakeLength = 3;

            snake = new Queue<SPoint>();
            head = new SPoint(new Point(mapHeight / 2, mapWidth / 2), snakeDir);
            snake.Enqueue(head);

            NewApple();

            Raylib.PlayMusicStream(msPlay);
        }

        private void SnakeMove(int pOffsetX, int pOffsetY)
        {
            SPoint newHead = new SPoint(new Point(head.p.X + pOffsetX, head.p.Y + pOffsetY),snakeDir);
           

            head = newHead;

            // GameOver ?? 
            // condition sortie ecran
            if(head.p.X < 0 || head.p.X > mapWidth - 1 || head.p.Y > mapHeight -1 || head.p.Y < 0) 
            {
                gameState = eGameState.gameOver;
            }
            // le serpent se mort
            if (isSnakeAt(head.p.Y, head.p.X))
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
            
            foreach (SPoint p in  snake)
            {
                if (p.p.X == pColonne && p.p.Y == pLigne) 
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
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
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
            // est ce que le serpent mange la sndPomme ?? 
            if (head.p == Apple) 
            {
                Raylib.PlaySound(sndPomme);
                NewApple();
                snakeLength++;
                score++;
            }
        }

        public Game()
        {
            // chargement des image
            texApple = Raylib.LoadTexture("images/apple.png");

            texSnakeHead = new TextureRL[4];
            texSnakeHead[(int)Direction.left] = new TextureRL("images/head_left.png");
            texSnakeHead[(int)Direction.right] = new TextureRL("images/head_right.png");
            texSnakeHead[(int)Direction.up] = new TextureRL("images/head_up.png");
            texSnakeHead[(int)Direction.down] = new TextureRL("images/head_down.png");

            texSnakeTail = new TextureRL[4];
            texSnakeTail[(int)Direction.left] = new TextureRL("images/tail_right.png");
            texSnakeTail[(int)Direction.right] = new TextureRL("images/tail_left.png");
            texSnakeTail[(int)Direction.up] = new TextureRL("images/tail_down.png");
            texSnakeTail[(int)Direction.down] = new TextureRL("images/tail_up.png");

            texSnakeBody = new TextureRL[12+1];
            texSnakeBody[3] = new TextureRL("images/body_topright.png");
            texSnakeBody[5] = new TextureRL("images/body_vertical.png");
            texSnakeBody[6] = new TextureRL("images/body_bottomright.png");
            texSnakeBody[9] = new TextureRL("images/body_topleft.png");
            texSnakeBody[10] = new TextureRL("images/body_horizontal.png");
            texSnakeBody[12] = new TextureRL("images/body_bottomleft.png");

            //chargement sons
            sndPomme = Raylib.LoadSound("sons/apple.wav");
            msPlay = Raylib.LoadMusicStream("sons/SnakeRaylib.mp3");
            msPlay.looping = true;

            _Init();
        }

        public void Load() 
        {
        }

        public void Update() 
        {
            Raylib.UpdateMusicStream(msPlay);

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
            Font fnt = Raylib.GetFontDefault();

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Raylib.WHITE);

            // Map
            for (int l = 0; l < mapHeight; l++)
            {
                for (int c = 0; c < mapWidth; c++)
                {
                    int x = c * _tailleCase;
                    int y = l * _tailleCase;
                    Raylib.DrawRectangleLines( x + marginWidth , y + marginHeight,_tailleCase-1, _tailleCase-1, Raylib.GRAY);
                }
            }

            // Apple
            Raylib.DrawTexture(texApple, Apple.X * _tailleCase + marginWidth, Apple.Y * _tailleCase + marginHeight, Raylib.WHITE);

            // Snake
            int index = 0;
            foreach (SPoint p in snake)
            {
                int x = p.p.X * _tailleCase;
                int y = p.p.Y * _tailleCase;

                if (p == snake.Last<SPoint>()) 
                {
                    Raylib.DrawTexture(texSnakeHead[(int)snakeDir].texture, x + marginWidth, y + marginHeight, Raylib.WHITE);
                }
                else if (p== snake.First<SPoint>()) 
                {
                    SPoint next = snake.ElementAt(1);
                    
                    Raylib.DrawTexture(texSnakeTail[(int)next.direction].texture, x + marginWidth, y + marginHeight, Raylib.WHITE);
                }
                else
                {
                    int mask = 0;

                    SPoint before = snake.ElementAt(index - 1);
                    SPoint after = snake.ElementAt(index + 1);

                    Point cup = new Point(p.p.X, p.p.Y - 1);
                    Point cdown = new Point(p.p.X, p.p.Y + 1);
                    Point cleft = new Point(p.p.X - 1, p.p.Y);
                    Point cright = new Point(p.p.X + 1, p.p.Y);

                    if (cup == before || cup == after) mask += 1;
                    if (cdown == before || cdown == after) mask += 4;
                    if (cleft == before || cleft == after) mask += 8;
                    if (cright == before || cright == after) mask += 2;

                    Raylib.DrawTexture(texSnakeBody[mask].texture, x + marginWidth, y + marginHeight, Raylib.WHITE);
                }
                index++;
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
            
            string Info = $"score : {score}     -     Etat du jeu : {statusJeu}";
            

            Vector2 txtScoreSize = Raylib.MeasureTextEx(fnt, Info, 30, 1);
            Vector2 pos = new Vector2((Raylib.GetScreenWidth() - txtScoreSize.X) / 2, (Raylib.GetScreenHeight() - txtScoreSize.Y * 2));

            var color = Raylib.DARKBLUE;

            if (gameState == eGameState.gameOver) 
            {
               color = Raylib.RED;
            }

            Raylib.DrawTextEx(fnt, Info, pos, 30, 1, color);

            Raylib.EndDrawing();
        }

        public void Unload() 
        {

            Raylib.UnloadTexture(texApple);

            foreach (TextureRL item in texSnakeBody)
            {
                if (item != null)
                    item.Free();
            }

            foreach (TextureRL item in texSnakeHead)
            {
                if (item != null)
                    item.Free();
            }

            foreach (TextureRL item in texSnakeTail)
            {
                if (item != null)
                    item.Free();
            }

            Raylib.StopMusicStream(msPlay);

            Raylib.UnloadSound(sndPomme);
            Raylib.UnloadMusicStream(msPlay);
        }
    }
}
