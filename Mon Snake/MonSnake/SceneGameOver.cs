using Raylib_cs;
using static Raylib_cs.Raylib;
using MonSnake.ORN;

namespace MonSnake;

public class SceneGameOver : Scene
{
    public const int MaxInputChars = 10;
    public char[] name = new char[MaxInputChars];
    public int letterCount = 0;

    Rectangle textBox = new(GetScreenWidth() / 2 - 100, 180, 225, 50);
    public bool mouseOnText = false;

    public int framesCounter = 0;
    
    public GameState GameState { get; init; }

    public SceneGameOver(GameState pGameState)
    {
        GameState = pGameState;
    }
    public override void Load()
    {
        
        base.Load();
    }

    public override void UnLoad()
    {
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        if (CheckCollisionPointRec(GetMousePosition(), textBox))
        {
            mouseOnText = true;
        }
        else
        {
            mouseOnText = false;
        }

        if (mouseOnText)
        {
            // Set the window's cursor to the I-Beam
            SetMouseCursor(MouseCursor.IBeam);

            // Check if more characters have been pressed on the same frame
            int key = GetCharPressed();

            while (key > 0)
            {
                // NOTE: Only allow keys in range [32..125]
                if ((key >= 32) && (key <= 125) && (letterCount < MaxInputChars))
                {
                    name[letterCount] = (char)key;
                    letterCount++;
                }

                // Check next character in the queue
                key = GetCharPressed();
            }

            if (IsKeyPressed(KeyboardKey.Backspace))
            {
                letterCount -= 1;
                if (letterCount < 0)
                {
                    letterCount = 0;
                }
                name[letterCount] = '\0';
            }
        }
        else
        {
            SetMouseCursor(MouseCursor.Default);
        }

        if (mouseOnText)
        {
            framesCounter += 1;
        }
        else
        {
            framesCounter = 0;
        }
        
        base.Update(dt);
    }

    public override void Draw()
    {
        DrawText("PLACE MOUSE OVER INPUT BOX!", 240, 140, 20, Color.Gray);
        DrawRectangleRec(textBox, Color.LightGray);

        if (mouseOnText)
        {
            DrawRectangleLines(
                (int)textBox.X,
                (int)textBox.Y,
                (int)textBox.Width,
                (int)textBox.Height,
                Color.Red
            );
        }
        else
        {
            DrawRectangleLines(
                (int)textBox.X,
                (int)textBox.Y,
                (int)textBox.Width,
                (int)textBox.Height,
                Color.DarkGray
            );
        }

        DrawText(new string(name), (int)textBox.X + 5, (int)textBox.Y + 8, 40, Color.Maroon);
        DrawText($"INPUT CHARS: {letterCount}/{MaxInputChars}", 315, 250, 20, Color.DarkGray);

        if (mouseOnText)
        {
            if (letterCount < MaxInputChars)
            {
                // Draw blinking underscore char
                if ((framesCounter / 20 % 2) == 0)
                {
                    DrawText(
                        "_",
                        (int)textBox.X + 8 + MeasureText(new string(name), 40),
                        (int)textBox.Y + 12,
                        40,
                        Color.Maroon
                    );
                }
            }
            else
            {
                DrawText("Press BACKSPACE to delete chars...", 230, 300, 20, Color.Gray);
            }
        }

        base.Draw();
    }
}