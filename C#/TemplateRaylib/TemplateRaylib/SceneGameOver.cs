using Raylib_cs;
using TemplateRaylib.ORN;

namespace TemplateRaylib;
using static Raylib_cs.Raylib;

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
        if (IsKeyPressed(KeyboardKey.Enter))
        {
            GameState.ChangeScene(GameState.SceneType.Menu);
        }
        
        base.Update(dt);
    }

    public override void Draw()
    {
        DrawText("GameOver", 190, 200, 20, Color.LightGray);

        base.Draw();
    }
}