using Godot;
using System;

public class RaycasterHelper : Reference 
{
    private const bool _debug = true;
    private static bool _ready = false;
    private static bool _printedChildMessage = false;
    private const string CHILD_MESSAGE = 
    "Required child nodes not found. Please make sure both a Sprite node" +
    " called 'Player' and a TileMap node called 'Level' are attached.";

    public static bool debug() 
    {
        return _debug;
    }

    public static void setReady(bool ready)
    {
        _ready = ready;
    }
    public static bool getReady()
    {
        return _ready;
    }

    public static void printChildMessage(bool success)
    {
        if (!_printedChildMessage)
        {
            if (success)
            {
                GD.Print("Player and Level nodes loaded.");
            } else {
                GD.Print(CHILD_MESSAGE);
            }
            _printedChildMessage = true;
        }
    }
}