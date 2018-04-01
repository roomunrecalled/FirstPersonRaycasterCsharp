using Godot;
using System;

public class raycaster : Node2D
{
    // Immutable Members
    private const bool debug = true;
    private const string CHILD_MESSAGE = 
    "Required child nodes not found. Please make sure both a Sprite node" +
    " called 'Player' and a TileMap node called 'Level' are attached.";

    // This can probably change on the fly, but I don't recommend it.
    private Vector2 screenDimensions;

    private bool childMessage;
    private bool ready;

    private int[,] level;

    private Vector2 playerPosition;
    private float playerRotation;

    // Called every time the node is added to the scene.
    public override void _Ready()
    {
        screenDimensions = GetViewport().Size;

        childMessage = false;
        ready = loadRequiredChildren();

        testMethod();
        if (debug) {
            GD.Print("DEBUG: Raycaster initialized.");
        }
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

    private void draw()
    {
        // If the player and level aren't loaded there's no point.
        if (!ready) return;

        // Call the actual draw function.
    }

    private bool loadRequiredChildren()
    {
        var player = (Sprite) GetNode("Player");
        var level = (TileMap) GetNode("Level");

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
        else
        {
            // Generate a matrix representation of the level.

            // Retrieve the player position & orientation.
            playerPosition = player.GetPosition();
            playerRotation = player.GetRotation();
        }

        if (debug && !childMessage) {
            GD.Print("Player and Level nodes loaded.");
            childMessage = true;
        }
        return result;
    }

    private int[,] loadLevel(TileMap tilemap) 
    {
        // Calculate tilemap dimensions
        Rect2 levelRect = tilemap.GetUsedRect();
        Vector2 levelDimensions = levelRect.Size;

        var level = new int[(int) levelDimensions.x, (int) levelDimensions.y];
        // initialize all elements in the created array to -1
        for (int i = 0; i < levelDimensions.x * levelDimensions.y; i += 1)
        {
            level[i%(int) levelDimensions.x, i/(int) levelDimensions.x] = -1;
        }

        foreach (Vector2 tilePosition in tilemap.GetUsedCells())
        {
            int tilesetIndex = tilemap.GetCellv(tilePosition);
            level[(int) tilePosition.x,(int) tilePosition.y] = tilesetIndex;
        }

        return level;
    }

    private void testMethod()
    {
        //draw();
        loadLevel((TileMap) GetNode("Level"));
    }

    private void drawTest() 
    {
        DrawRect(new Rect2(new Vector2(0,0), new Vector2(30,30)), new Color(0,244,0));
    }


}
