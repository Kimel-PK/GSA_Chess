using System;

/// <summary>
/// Single spawn data info, used in BoardDataScriptableObject
/// </summary>
[Serializable]
public class SpawnData
{
    public int playerNumber;
    public BoardTileType boardTileType;
    public int amount;
    public Direction direction;
}
