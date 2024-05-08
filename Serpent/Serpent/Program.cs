using Raylib_CsLo;
using Texture2D = Raylib_CsLo.Texture;

namespace Serpent
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            const int screenWidth = 800;
            const int screenHeight = 600;

            // creation fenetre 
            Raylib.InitWindow(screenWidth, screenHeight, "Projet 1 : SNAKE  v1.0 05/2024");
            Raylib.SetTargetFPS(60);

            Game monJeu = new Game();

            monJeu.Load();

            // Game loop
            while (!Raylib.WindowShouldClose())
            {

                // Update 
                monJeu.Update();

                // Draw 
                monJeu.Draw();
            }

            monJeu.Unload();
            Raylib.CloseWindow();

            return Task.CompletedTask;
        }
    }
}