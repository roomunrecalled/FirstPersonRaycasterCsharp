using Godot;
using System;

public class raycaster : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        GD.Print("Hello from C#.");
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(new Vector2(0,0), new Vector2(30,30)), new Color(0,244,0));
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
