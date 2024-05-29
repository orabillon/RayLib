using Raylib_cs;
using TemplateRaylib;
using static Raylib_cs.Raylib;

// Initialization
//--------------------------------------------------------------------------------------
const int screenWidth = 800;
const int screenHeight = 600;

Game monJeu = new Game();

InitWindow(screenWidth, screenHeight, "raylib Template");

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
//--------------------------------------------------------------------------------------

// Permet de jouer des son et regler le volume
InitAudioDevice();
SetMasterVolume(0.05f);  

// chargement des ressource
    monJeu.Load();

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
    monJeu.Update(GetFrameTime());

    monJeu.Draw();
}

// De-Initialization
//--------------------------------------------------------------------------------------
monJeu.Unload();
CloseAudioDevice();
CloseWindow();        // Close window and OpenGL context
//--------------------------------------------------------------------------------------

return 0;