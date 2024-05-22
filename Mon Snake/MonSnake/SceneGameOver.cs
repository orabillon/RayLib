using Raylib_cs;
using MonSnake.ORN;

namespace MonSnake;

public class SceneGameOver : Scene
{
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
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            GameState.ChangeScene(GameState.SceneType.Menu);
        }
        
        base.Update(dt);
    }

    public override void Draw()
    {
        Raylib.DrawText("GameOver", 190, 200, 20, Color.LightGray);

        base.Draw();
    }
}