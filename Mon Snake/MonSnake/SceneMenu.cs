using System.Numerics;
using Raylib_cs;
using MonSnake.ORN;

namespace MonSnake;

public class SceneMenu : Scene
{
    public GameState GameState { get; init; }
    public int ScreenWidth { get; init; }
    public int ScreenHeight { get; init; }

    public FontRl fnt;
    
    MusicRl msMenu;
    
    private Button _button;

    // Variable pour titre menu
    public Color[] LstColor = new[]
    {
        Color.Gold,
        Color.Blue,
        Color.Green,
        Color.Orange,
        Color.Purple
    };

    public float MenuSin { get; set; } = 4;
    public float AmplitudeSin { get; set; } = 45;

    public float CoulDelay { get; set; } = 0.35f;
    public float CoulDelayTitre { get; set; }
    public int CoulSnakeTitre { get; set; }
    
    
    public SceneMenu(GameState pGameState)
    {
        GameState = pGameState;
        ScreenHeight = Raylib.GetScreenHeight();
        ScreenWidth = Raylib.GetScreenWidth();
    }
    public override void Load()
    {
        _button = new Button("assets/images/button.png");
        _button.onClick = onClikPlay;
        _button.Position = new Vector2(
            ScreenWidth / 2 - _button.Texture.Texture.Width / 2, 
            ScreenHeight - 2 * _button.Texture.Texture.Height 
        );
        listActors.Add(_button);
        fnt = new FontRl("assets/font/SnakeBusiness.ttf", 100);
        CoulSnakeTitre = 0;
        CoulDelayTitre= CoulDelay;
        
        msMenu = new MusicRl("assets/sons/Menu.mp3");
        Raylib.PlayMusicStream(msMenu.Music);
        
        base.Load();
    }

    public override void UnLoad()
    {
        _button.Unload();
        fnt.Free();
        Raylib.StopMusicStream(msMenu.Music);
        msMenu.Free();
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        MenuSin = MenuSin + 3 * 60 * dt;

        CoulDelayTitre -= dt;
        if (CoulDelayTitre <= 0)
        {
            CoulDelayTitre = CoulDelay;
            CoulSnakeTitre++;

            if (CoulSnakeTitre >= LstColor.Count())
            {
                CoulSnakeTitre = 0;
            }
                
        }
        
        Raylib.UpdateMusicStream(msMenu.Music);
        
        base.Update(dt);
    }

    public override void Draw()
    {
        String Titre = "(snake)";

        Vector2 txtSizeT = Raylib.MeasureTextEx(fnt.Font, Titre, 100, 0);
        float w = txtSizeT.X;
        float h = txtSizeT.Y;
        float x = (ScreenWidth - w) / 2;
        float y = 0;
        
        y = (float)(Math.Sin((x + MenuSin) / 50) * AmplitudeSin); 
        Raylib.DrawTextEx(fnt.Font,Titre, new Vector2(x, y + (ScreenHeight - h) /2), 100,1, LstColor[CoulSnakeTitre]);
        x = x + (Raylib.MeasureTextEx(fnt.Font, Titre, 100, 0).X) + 10;
        
        base.Draw();
    }
    
    public void onClikPlay(Button pSender)
    {
        GameState.ChangeScene(GameState.SceneType.Gameplay);
    }
}