using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MonSnake.ORN;

// delege pour definir les action du bouton
public delegate void OnClick(Button pSender);
public delegate void OnMouseOver(Button pSender);
public delegate void OnMouseEnter(Button pSender);
public delegate void OnMouseLeave(Button pSender);

/// <summary>
/// Classe pour la gestion de bouton
/// </summary>
public class Button : Sprite
{
    public bool isHover {  get; private set; }
    public bool isEnter { get; private set; }
    public OnClick onClick { get; set; }
    public OnMouseOver OnMouseOver { get; set; }
    public OnMouseEnter OnMouseEnter { get; set; } 
    public OnMouseLeave OnMouseLeave { get; set; } 
    
    public Button(string pPath) : base(pPath)
    {
    }

    public override void Update(float dt)
    {
       Vector2 MousePos = GetMousePosition();

        if(CheckCollisionPointRec(MousePos, BoundingBox))
        {
            if (!isHover)
            {
                isHover = true;
                if(OnMouseOver != null) {  OnMouseOver(this); }

                if (!isEnter)
                {
                    isEnter = true;
                    if (OnMouseEnter != null) { OnMouseEnter(this); }
                }
                
                Debug.WriteLine("Entrer dans le boutton"); 
            }
        }
        else
        {
            if (isHover) {    
                Debug.WriteLine("Sortie du boutton");
            }
            
            isHover = false;
            isEnter = false;

            if (OnMouseLeave != null) { OnMouseLeave(this); }
            
        }

        if(isHover)
        {
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                  
                if(onClick != null) {  onClick(this); }
                Debug.WriteLine("Click");
            }
        }

        base.Update(dt);
    }
    
}