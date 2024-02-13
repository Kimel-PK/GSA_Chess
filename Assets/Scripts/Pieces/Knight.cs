using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override bool IsValidMove(Vector2Int movePosition)
    {
        return true;
    }

    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> results = new();
        List<Vector2Int> possibleMoves = new()
        {
            Position + new Vector2Int(-2, -1),
            Position + new Vector2Int(-2, 1),
            Position + new Vector2Int(-1, -2),
            Position + new Vector2Int(-1, 2),
            Position + new Vector2Int(1, -2),
            Position + new Vector2Int(1, 2),
            Position + new Vector2Int(2, -1),
            Position + new Vector2Int(2, 1)
        };
        
        foreach (Vector2Int possibleMove in possibleMoves)
        {
            BoardTile tile = board.GetTileAt(possibleMove);
            // TODO check if there is a piece at that tile and whether it's an enemy piece
            if (tile)
                results.Add(possibleMove);
        }
        return results;
    }
}
