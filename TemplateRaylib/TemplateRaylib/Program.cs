using Raylib_cs;

// Initialization
//--------------------------------------------------------------------------------------
const int screenWidth = 800;
const int screenHeight = 600;

Raylib.InitWindow(screenWidth, screenHeight, "raylib Template");

Raylib.SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
//--------------------------------------------------------------------------------------

// Main game loop
while (!Raylib.WindowShouldClose())    // Detect window close button or ESC key
{
    // Update
    //----------------------------------------------------------------------------------
    // TODO: Update your variables here
    //----------------------------------------------------------------------------------

    // Draw
    //----------------------------------------------------------------------------------
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.White);

    Raylib.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.LightGray);

    Raylib.EndDrawing();
    //----------------------------------------------------------------------------------
}

// De-Initialization
//--------------------------------------------------------------------------------------
Raylib.CloseWindow();        // Close window and OpenGL context
//--------------------------------------------------------------------------------------

return 0;