using Godot;
using System;

public class raycaster : Node2D
{
    // Immutable Members
    private const bool debug = true;
    private const string CHILD_MESSAGE = 
    "Required child nodes not found. Please make sure both a Sprite node" +
    " called 'Player' and a TileMap node called 'Level' are attached.";

    private bool childMessage;
    private bool ready;

    private Sprite player;
    private TileMap level;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        if (debug) {
            GD.Print("DEBUG: Raycaster initialized.");
        }
        childMessage = false;

        ready = loadRequiredChildren();

        testMethod();
    }

    public override void _Draw()
    {
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        Update();
    }

    private void testMethod()
    {
        draw();
    }

    private void drawTest() 
    {
        DrawRect(new Rect2(new Vector2(0,0), new Vector2(30,30)), new Color(0,244,0));
    }

    private void draw()
    {
    }

    private bool loadRequiredChildren()
    {
        player = (Sprite) GetNode("Player");
        level = (TileMap) GetNode("Level");

        bool result = true;

        if ((player == null || level == null)  || 
                (!player.GetClass().Equals("Sprite") ||
                !level.GetClass().Equals("TileMap")))
        {
            if (!childMessage) 
            {
                GD.Print(CHILD_MESSAGE);
                childMessage = true;
            }
            result = false;
        }
        if (debug && !childMessage) {
            GD.Print("Player and Level nodes loaded.");
            childMessage = true;
        }
        return result;
    }
}
