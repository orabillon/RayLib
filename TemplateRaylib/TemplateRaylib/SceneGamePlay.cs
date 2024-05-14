﻿using Raylib_cs;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class SceneGamePlay : Scene
{
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
        base.Update(dt);
    }

    public override void Draw()
    {
        Raylib.DrawText("GamePlay", 190, 200, 20, Color.LightGray);

        base.Draw();
    }
}