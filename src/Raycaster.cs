using Godot;
using System;

public class Raycaster : Node2D
{
    // Logistical Variables
    private readonly RaycasterUtil util = new Raycaster.RaycasterUtil();

    // World Attributes
    private const float worldCubeSize = 64;
    private float tileGridSize;
    private int[,] levelMatrix;

    // Player Attributes
    private Sprite playerReference;
    //private Vector2 playerPositionOnTileMap;
    //private Vector2 playerSightUnitVector

    // Projection Attributes
    private Vector2 screenDimensions;
    private const float FovRadians = (float) 1.4; // ~80 degrees
    // Direction player is facing & distance to camera plane
    private Vector2 playerDirectionVector = 
        new Vector2(0,1);
    // The 'camera plane' can be represented as a line, so we'll use a Vector2
    private Vector2 cameraPlaneVector = 
        new Vector2((float) Math.Tan(FovRadians),0);

    // Called every time the node is added to the scene.
    public override void _Ready()
    {
        screenDimensions = GetViewport().Size;

        if (loadRequiredChildren())
        {
            util.setReady();
        }

        testMethod();
        if (util.debugEnabled()) {
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
        if (!util.isReady()) return;

        // Call the actual draw function.
        for (int y = 0; y < screenDimensions.y; y += 1)
        {
            drawColumn(y);
        }
    }

    private void drawColumn(int y)
    {
        // castRay()
        // determineDistanceToWall()
        // drawWallSlice()
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
            if (!util.childMessagePrinted()) 
            {
                GD.Print(util.getChildMessage());
                util.toggleChildMessage();
            }
            result = false;
        } 
        else
        {
            // Generate a matrix representation of the level.
            tileGridSize = level.GetCellSize().x;
            levelMatrix = createLevelFromTileMap(level);

            // Get a reference to the player 
            playerReference = player;

            // Hide the level & player
            level.Hide();
            player.Hide();
        }

        if (util.debugEnabled() && !util.childMessagePrinted()) {
            GD.Print("Player and Level nodes loaded.");
            util.toggleChildMessage();
        }
        return result;
    }

    private int[,] createLevelFromTileMap(TileMap tilemap) 
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
        createLevelFromTileMap((TileMap) GetNode("Level"));
    }

    private void drawTest() 
    {
        DrawRect(new Rect2(new Vector2(0,0), new Vector2(30,30)), new Color(0,244,0));
    }

    class RaycasterUtil
    {
        // Parent Reference;
        private Raycaster casterParent;
        // Logistical Variables
        // Immutable Members
        private const bool debug = true;
        private const string CHILD_MESSAGE = 
        "Required child nodes not found. Please make sure both a Sprite node" +
        " called 'Player' and a TileMap node called 'Level' are attached.";
        // Mutable Logistical Variables
        private bool childMessage = false;
        private bool ready = false;

        public RaycasterUtil() { }
        public RaycasterUtil(Raycaster parent)
        {
            this.casterParent = parent;
        }

        public void toggleChildMessage() { childMessage = true; }
        public String getChildMessage() { return CHILD_MESSAGE; }
        public bool childMessagePrinted() { return childMessage; }

        public void setReady() { ready = true; }
        public bool isReady() { return ready; }

        public bool debugEnabled() { return debug; }

        public float tile2World(float lengthInPixels)
        {
            return (lengthInPixels/casterParent.tileGridSize) 
                * Raycaster.worldCubeSize;
        }
        public float world2Tile(float lengthInUnits)
        {
            return (lengthInUnits/Raycaster.worldCubeSize) 
                * casterParent.tileGridSize;
        }
    }
}
