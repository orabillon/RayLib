using Raylib_cs;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class Game
{
    public GameState GameState { get; init; }

    public Game()
    {
        GameState = new GameState ();
    }
    public void Load()
    {
        GameState.ChangeScene(GameState.SceneType.Gameplay);
        
        if (GameState != null)
        {
            GameState.CurrentScene.Load();
        }
    }

    public void Update(float dt)
    {
        if (GameState != null)
        {
            GameState.CurrentScene.Update(dt);
        }
    }

    public void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);
        
        if (GameState != null)
        {
            GameState.CurrentScene.Draw();
        }
        
        Raylib.EndDrawing();
    }

    public void Unload()
    {
        if (GameState != null)
        {
            GameState.CurrentScene.UnLoad();
        }
    }
}