using System.Numerics;
using Raylib_cs;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class SceneGamePlay : Scene
{
    private Hero Hero;
    
    public override void Load()
    {
        Hero = new Hero("assets/images/ship.png", 100, 100);
        Hero.Position = new Vector2(Raylib.GetScreenWidth()/2 - Hero.Texture.Texture.Width / 2, Raylib.GetScreenHeight()/2 - Hero.Texture.Texture.Height / 2);
        listActors.Add(Hero);
        base.Load();
    }

    public override void UnLoad()
    {
        Hero.Texture.Free();
        base.UnLoad();
    }

    public override void Update(float dt)
    {
        Hero.Update(dt);
        base.Update(dt);
    }

    public override void Draw()
    {
        Raylib.DrawText("GamePlay", 1, 1, 20, Color.LightGray);
        
        base.Draw();
    }
}