using Raylib_cs;

namespace TemplateRaylib;

public class Game
{
    public void Load()
    {
        
    }

    public void Update(float dt)
    {
        
    }

    public void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);

        Raylib.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.LightGray);

        Raylib.EndDrawing();
    }

    public void Unload()
    {
        
    }
}