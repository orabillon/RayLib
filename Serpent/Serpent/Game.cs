using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Texture2D = Raylib_CsLo.Texture;

namespace Serpent
{
    public class Game
    {
        Vector2 position;

        Texture2D texture;

        int vitesse = 3;

        public void Load() 
        {
            texture = Raylib.LoadTexture("images/ship.png");
            position = new Vector2(50,50);
        }

        public void Update() 
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
               position.Y -= vitesse;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                position.X -= vitesse;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                position.Y += vitesse;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                position.X += vitesse;
            }
        }

        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Raylib.BLACK);

            Raylib.DrawText("Congrats! You created your first window!", 19, 19, 20, Raylib.WHITE);

            Raylib.DrawTextureEx(texture, position,20,1,Raylib.WHITE);

            Raylib.EndDrawing();
        }

        public void Unload() 
        {
            Raylib.UnloadTexture(texture);
        }
    }
}
