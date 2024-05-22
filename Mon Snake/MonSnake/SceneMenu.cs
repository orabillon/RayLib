using System.Numerics;
using Raylib_cs;
using MonSnake.ORN;

namespace MonSnake;

public class SceneMenu : Scene
{
    public GameState GameState { get; init; }
    
    private Button Button;
    
    public SceneMenu(GameState pGameState)
    {
        GameState = pGameState;
    }
    public override void Load()
    {
        Button = new Button("assets/images/button.png");
        Button.onClick = onClikPlay;
        Button.Position = new Vector2(
            Raylib.GetScreenWidth() / 2 - Button.Texture.Texture.Width / 2, 
            Raylib.GetScreenHeight() / 2 -  Button.Texture.Texture.Height /2
        );
        listActors.Add(Button);
        base.Load();
    }

    public override void UnLoad()
    {
        Button.Unload();
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        base.Update(dt);
    }

    public override void Draw()
    {
        base.Draw();
    }
    
    public void onClikPlay(Button pSender)
    {
        GameState.ChangeScene(GameState.SceneType.Gameplay);
    }
}