using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;
using Raylib_cs;
using MonSnake.ORN;
using Color = Raylib_cs.Color;

namespace MonSnake;

public class SceneGamePlay : Scene
{
    public GameState GameState { get; init; }
    
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
    TextureRl[] texSnakeHead;
    TextureRl[] texSnakeTail;
    TextureRl[] texSnakeBody;

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
            snakeDir = Direction.RIGHT;
            NextDirection = Direction.RIGHT;
            gameState = eGameState.PAUSE;
            snakeLength = 3;

            snake = new Queue<SPoint>();
            head = new SPoint(new Point(mapHeight / 2, mapWidth / 2), snakeDir);
            snake.Enqueue(head);

            NewApple();

            Raylib.PlayMusicStream(msPlay);
        }

        private void SnakeMove(int pOffsetX, int pOffsetY)
        {
            SPoint newHead = new SPoint(new Point(head.point.X + pOffsetX, head.point.Y + pOffsetY),snakeDir);
           

            head = newHead;

            // GameOver ?? 
            // condition sortie ecran
            if(head.point.X < 0 || head.point.X > mapWidth - 1 || head.point.Y > mapHeight -1 || head.point.Y < 0) 
            {
                gameState = eGameState.GAMEOVER;
            }
            // le serpent se mort
            if (isSnakeAt(head.point.Y, head.point.X))
            {
                gameState = eGameState.GAMEOVER;
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
                if (p.point.X == pColonne && p.point.Y == pLigne) 
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

            if (Raylib.IsKeyDown(KeyboardKey.Right) && (snakeDir == Direction.UP || snakeDir == Direction.DOWN))
            { NextDirection = Direction.RIGHT; }
            if (Raylib.IsKeyDown(KeyboardKey.Left) && (snakeDir == Direction.UP || snakeDir == Direction.DOWN))
            { NextDirection = Direction.LEFT; }
            if (Raylib.IsKeyDown(KeyboardKey.Up) && (snakeDir == Direction.LEFT || snakeDir == Direction.RIGHT))
            { NextDirection = Direction.UP; }
            if (Raylib.IsKeyDown(KeyboardKey.Down) && (snakeDir == Direction.LEFT || snakeDir == Direction.RIGHT))
            { NextDirection = Direction.DOWN; }
            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            { gameState = eGameState.PAUSE; }

            if (timer >= snakeSpeed)
            {
                timer = 0;
                snakeDir = NextDirection;
                switch (snakeDir)
                {
                    case Direction.LEFT:
                        SnakeMove(-1, 0);

                        break;
                    case Direction.UP:
                        SnakeMove(0, -1);
                        break;
                    case Direction.RIGHT:
                        SnakeMove(1, 0);
                        break;
                    case Direction.DOWN:
                        SnakeMove(0, 1);
                        break;
                    default:
                        break;
                }
            }
            // est ce que le serpent mange la sndPomme ?? 
            if (head.point == Apple) 
            {
                Raylib.PlaySound(sndPomme);
                NewApple();
                snakeLength++;
                score++;
            }
        }

    public SceneGamePlay(GameState pGameState)
    {
        GameState = pGameState;
    }
    
    public override void Load()
    {
        // chargement des image
        texApple = Raylib.LoadTexture("assets/images/apple.png");

        texSnakeHead = new TextureRl[4];
        texSnakeHead[(int)Direction.LEFT] = new TextureRl("assets/images/head_left.png");
        texSnakeHead[(int)Direction.RIGHT] = new TextureRl("assets/images/head_right.png");
        texSnakeHead[(int)Direction.UP] = new TextureRl("assets/images/head_up.png");
        texSnakeHead[(int)Direction.DOWN] = new TextureRl("assets/images/head_down.png");

        texSnakeTail = new TextureRl[4];
        texSnakeTail[(int)Direction.LEFT] = new TextureRl("assets/images/tail_right.png");
        texSnakeTail[(int)Direction.RIGHT] = new TextureRl("assets/images/tail_left.png");
        texSnakeTail[(int)Direction.UP] = new TextureRl("assets/images/tail_down.png");
        texSnakeTail[(int)Direction.DOWN] = new TextureRl("assets/images/tail_up.png");

        texSnakeBody = new TextureRl[12+1];
        texSnakeBody[3] = new TextureRl("assets/images/body_topright.png");
        texSnakeBody[5] = new TextureRl("assets/images/body_vertical.png");
        texSnakeBody[6] = new TextureRl("assets/images/body_bottomright.png");
        texSnakeBody[9] = new TextureRl("assets/images/body_topleft.png");
        texSnakeBody[10] = new TextureRl("assets/images/body_horizontal.png");
        texSnakeBody[12] = new TextureRl("assets/images/body_bottomleft.png");

        //chargement sons
        sndPomme = Raylib.LoadSound("assets/sons/apple.wav");
        msPlay = Raylib.LoadMusicStream("assets/sons/SnakeRaylib.mp3");
        msPlay.Looping = true;

        _Init();
        
        base.Load();
    }

    public override void UnLoad()
    {
        
        Raylib.UnloadTexture(texApple);

        foreach (TextureRl item in texSnakeBody)
        {
            if (item != null)
                item.Free();
        }

        foreach (TextureRl item in texSnakeHead)
        {
            if (item != null)
                item.Free();
        }

        foreach (TextureRl item in texSnakeTail)
        {
            if (item != null)
                item.Free();
        }

        Raylib.StopMusicStream(msPlay);
        
        Raylib.UnloadSound(sndPomme);
        Raylib.UnloadMusicStream(msPlay);
        
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        Raylib.UpdateMusicStream(msPlay);

        switch (gameState)
        {
            case eGameState.PLAY: 
                Play();
                break;
            case eGameState.PAUSE:
                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                { gameState = eGameState.PLAY; }
                break;
        }
        
        Clean();
        
        base.Update(dt);
    }

    public override void Draw()
    {
        Font fnt = Raylib.GetFontDefault();
        
         // Map
            for (int l = 0; l < mapHeight; l++)
            {
                for (int c = 0; c < mapWidth; c++)
                {
                    int x = c * _tailleCase;
                    int y = l * _tailleCase;
                    Raylib.DrawRectangleLines( x + marginWidth , y + marginHeight,_tailleCase-1, _tailleCase-1, Raylib_cs.Color.Gray);
                }
            }

            // Apple
            Raylib.DrawTexture(texApple, Apple.X * _tailleCase + marginWidth, Apple.Y * _tailleCase + marginHeight, Raylib_cs.Color.White);

            // Snake
            int index = 0;
            foreach (SPoint p in snake)
            {
                int x = p.point.X * _tailleCase;
                int y = p.point.Y * _tailleCase;

                if (p == snake.Last<SPoint>()) 
                {
                    Raylib.DrawTexture(texSnakeHead[(int)snakeDir].Texture, x + marginWidth, y + marginHeight, Raylib_cs.Color.White);
                }
                else if (p== snake.First<SPoint>()) 
                {
                    SPoint next = snake.ElementAt(1);
                    
                    Raylib.DrawTexture(texSnakeTail[(int)next.direction].Texture, x + marginWidth, y + marginHeight, Raylib_cs.Color.White);
                }
                else
                {
                    int mask = 0;

                    SPoint before = snake.ElementAt(index - 1);
                    SPoint after = snake.ElementAt(index + 1);

                    Point cup = new Point(p.point.X, p.point.Y - 1);
                    Point cdown = new Point(p.point.X, p.point.Y + 1);
                    Point cleft = new Point(p.point.X - 1, p.point.Y);
                    Point cright = new Point(p.point.X + 1, p.point.Y);

                    if (cup == before || cup == after) mask += 1;
                    if (cdown == before || cdown == after) mask += 4;
                    if (cleft == before || cleft == after) mask += 8;
                    if (cright == before || cright == after) mask += 2;

                    Raylib.DrawTexture(texSnakeBody[mask].Texture, x + marginWidth, y + marginHeight, Raylib_cs.Color.White);
                }
                index++;
            }

            string statusJeu = string.Empty;
            switch (gameState)
            {
                case eGameState.PLAY: 
                    statusJeu = "Play";
                    break;
                case eGameState.PAUSE:
                    statusJeu = "Pause";
                    break;
                case eGameState.GAMEOVER:
                    statusJeu = "GameOver";
                    break;
            }
            
            string Info = $"score : {score}     -     Etat du jeu : {statusJeu}";
            

            Vector2 txtScoreSize = Raylib.MeasureTextEx(fnt, Info, 30, 1);
            Vector2 pos = new Vector2((Raylib.GetScreenWidth() - txtScoreSize.X) / 2, (Raylib.GetScreenHeight() - txtScoreSize.Y * 2));

            var color = Raylib_cs.Color.DarkBlue;

            if (gameState == eGameState.GAMEOVER) 
            {
                GameState.ChangeScene(GameState.SceneType.Gameover);
            }

            Raylib.DrawTextEx(fnt, Info, pos, 30, 1, color);
        
        base.Draw();
    }
    
    
}