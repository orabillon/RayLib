using Raylib_cs;
using MonSnake;

// Initialization
//--------------------------------------------------------------------------------------
const int screenWidth = 800;
const int screenHeight = 600;

Game monJeu = new Game();

Raylib.InitWindow(screenWidth, screenHeight, "raylib - Mon Snake 1.0");

Raylib.SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
//--------------------------------------------------------------------------------------

// Permet de jouer des son et regler le volume
Raylib.InitAudioDevice();
Raylib.SetMasterVolume(0.5f);  

// chargement des ressource
    monJeu.Load();

// Main game loop
while (!Raylib.WindowShouldClose())    // Detect window close button or ESC key
{
    monJeu.Update(Raylib.GetFrameTime());

    monJeu.Draw();
}

// De-Initialization
//--------------------------------------------------------------------------------------
monJeu.Unload();
Raylib.CloseAudioDevice();
Raylib.CloseWindow();        // Close window and OpenGL context
//--------------------------------------------------------------------------------------

return 0;