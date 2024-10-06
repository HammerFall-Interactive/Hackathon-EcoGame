using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemState
{
    public static bool SnakePlayed { get; set; } = false;
    public static bool ToolReady { get; set; } = false;
    public static bool PortalsVisited { get; set; } = false;
}
