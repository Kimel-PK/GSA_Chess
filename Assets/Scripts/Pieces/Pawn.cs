using System;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override bool IsValidMove(Vector2Int movePosition)
    {
        return GetValidMoves().Contains(movePosition);
    }

    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new();
        Vector2Int forward = Position;
        switch (PieceDirection)
        {
            case Direction.Up:
                forward += new Vector2Int(0, 1);
                break;
            case Direction.Right:
                forward += new Vector2Int(1, 0);
                break;
            case Direction.Down:
                forward += new Vector2Int(0, -1);
                break;
            case Direction.Left:
                forward += new Vector2Int(-1, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        validMoves.Add(forward);
        return validMoves;
    }
}