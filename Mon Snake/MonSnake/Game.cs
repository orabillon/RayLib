using Raylib_cs;
using MonSnake.ORN;

namespace MonSnake;
public enum Direction {
    LEFT,UP,RIGHT,DOWN
}

enum eGameState
{
    PLAY,
    PAUSE,
    GAMEOVER
}
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
        GameState.ChangeScene(GameState.SceneType.Menu);
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
        
         // liberation des ressources partager 
        AssetsManager.Unload();
    }
}