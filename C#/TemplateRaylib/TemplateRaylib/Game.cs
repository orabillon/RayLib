using Raylib_cs;
using TemplateRaylib.ORN;

namespace TemplateRaylib;
using static Raylib_cs.Raylib;

public class Game
{
    public GameState GameState { get; init; }

    public Game()
    {
        GameState = new GameState ();
    }
    public void Load()
    {
        // Chargement ressource partager
        AssetsManager.Load();
        GameState.ChangeScene(GameState.SceneType.Gameover);
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
        BeginDrawing();

        ClearBackground(Color.White);
        
        if (GameState != null)
        {
            GameState.CurrentScene.Draw();
        }
        EndDrawing();
    }

    public void Unload()
    {
        if (GameState != null)
        {
            GameState.CurrentScene.UnLoad();
        }
    }
}