using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class SceneMenu : Scene
{
    public GameState GameState { get; init; }
    
    private Button Button;
    private MusicRl muMusic;
    
    public SceneMenu(GameState pGameState)
    {
        GameState = pGameState;
    }
    public override void Load()
    {
        Button = new Button("assets/images/button.png");
        Button.onClick = onClikPlay;
        Button.Position = new Vector2(
            GetScreenWidth() / 2 - Button.Texture.Texture.Width / 2, 
            GetScreenHeight() / 2 -  Button.Texture.Texture.Height /2
        );
        listActors.Add(Button);
        muMusic = new MusicRl("assets/sons/cool.mp3");
        PlayMusicStream(muMusic.Music);
        base.Load();
    }

    public override void UnLoad()
    {
        Button.Unload();
        StopMusicStream(muMusic.Music);
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        UpdateMusicStream(muMusic.Music);
        base.Update(dt);
    }

    public override void Draw()
    {
        DrawText("Menu", 190, 200, 20, Color.LightGray);

        base.Draw();
    }
    
    public void onClikPlay(Button pSender)
    {
        GameState.ChangeScene(GameState.SceneType.Gameplay);
    }
}