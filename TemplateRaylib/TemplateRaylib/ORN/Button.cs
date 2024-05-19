using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TemplateRaylib.ORN;

// delege pour definir l'action au click
public delegate void OnClick(Button pSender);
/// <summary>
/// Classe pour la gestion de bouton
/// </summary>
public class Button : Sprite
{
    public bool isHover {  get; private set; }
    public OnClick onClick { get; set; }
    
    public Button(string pPath) : base(pPath)
    {
    }

    public override void Update(float dt)
    {
       Vector2 MousePos = Raylib.GetMousePosition();

        if(Raylib.CheckCollisionPointRec(MousePos, BoundingBox))
        {
            if (!isHover)
            {
                isHover = true;
                Debug.WriteLine("Entrer dans le boutton"); 
            }
        }
        else
        {
            if (isHover) {    
                Debug.WriteLine("Sortie du boutton");
            }
            isHover = false;
        }

        if(isHover)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                  
                if(onClick != null) {  onClick(this); }
                Debug.WriteLine("Click");
            }
        }

        base.Update(dt);
    }

    public override void Draw()
    {
        base.Draw();
    }

}