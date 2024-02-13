using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override bool IsValidMove(Vector2Int movePosition)
    {
        return true;
    }

    public override List<Vector2Int> GetValidMoves()
    {
        // TODO implement
        return new List<Vector2Int>();
    }
}
