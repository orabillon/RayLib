using System.Collections.Specialized;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class SceneGamePlay : Scene
{
    private Hero Hero;
    private SoundRl sndMeteore;
    private MusicRl muMusic;
    
    public GameState GameState { get; init; }

    public SceneGamePlay(GameState pGameState)
    {
        GameState = pGameState;
    }
    
    public override void Load()
    {
        // personage
        Hero = new Hero("assets/images/ship.png");
        Hero.Position = new Vector2(GetScreenWidth()/2 - Hero.Texture.Texture.Width / 2, GetScreenHeight()/2 - Hero.Texture.Texture.Height / 2);
        listActors.Add(Hero);
        
        // Meteor
        for (int i = 0; i < 10; i++)
        {
            Meteor m = new Meteor("assets/images/meteor.png");
            int x = Utils.GetInt(0, Raylib.GetScreenWidth() - m.Texture.Texture.Width);
            int y = Utils.GetInt(0, Raylib.GetScreenHeight() - m.Texture.Texture.Height);
            m.Position = new Vector2(x, y);
            listActors.Add(m);
        }

        sndMeteore = new SoundRl("assets/sons/explode.wav");
        muMusic = new MusicRl("assets/sons/techno.mp3");
        PlayMusicStream(muMusic.Music);
        
        base.Load();
    }

    public override void UnLoad()
    {
        Hero.Unload();
        sndMeteore.Free();
        StopMusicStream(muMusic.Music);
        muMusic.Free();
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        foreach (var actor in listActors)
        {
            if(actor is Meteor m) 
            {
                // Limite Ecran 
                if (m.Position.X < 0)
                {
                    m.Position = new Vector2(0, m.Position.Y);
                    m.VelociteX *= -1;
                }
                if(m.Position.X + m.BoundingBox.Width > GetScreenWidth()) {
                    m.VelociteX *= -1;
                    m.Position = new Vector2(GetScreenWidth() - m.BoundingBox.Width, m.Position.Y);
                }
                if (m.Position.Y < 0)
                {
                    m.Position = new Vector2(m.Position.X, 0);
                    m.VelociteY *= -1;
                }
                if (m.Position.Y + m.BoundingBox.Height > GetScreenHeight())
                {
                    m.VelociteY *= -1;
                    m.Position = new Vector2(m.Position.X, GetScreenHeight() - m.BoundingBox.Height);

                }
                
                // Test Colision avec le hero
                if(Utils.CollideByBox(m, Hero))
                {
                    Hero.TouchedBy(m);
                    m.TouchedBy(Hero);

                    if (Hero.Energie < 0)
                    {
                        GameState.ChangeScene(GameState.SceneType.Gameover);
                    }

                    m.ToRemove = true;
                    PlaySound(sndMeteore.Sound);
                }
            }
                
        }
        
        // Deplacement hero
        if (IsKeyDown(KeyboardKey.Right))
        {
            Hero.Move(1, 0);
        }
        if (IsKeyDown(KeyboardKey.Left))
        {
            Hero.Move(-1, 0);
        }
        if (IsKeyDown(KeyboardKey.Up))
        {
            Hero.Move(0, -1);
        }
        if (IsKeyDown(KeyboardKey.Down))
        {
            Hero.Move(0, 1);
        }
        
        Clean();
        
        if(listActors.Count == 1)
            GameState.ChangeScene(GameState.SceneType.Menu);
        
        UpdateMusicStream(muMusic.Music);
        
        base.Update(dt);
    }

    public override void Draw()
    {
        DrawTextEx(AssetsManager.MainFont, "GamePlay", new Vector2(1,1), 30, 1,Color.LightGray);
        
        base.Draw();
    }
    
    
}