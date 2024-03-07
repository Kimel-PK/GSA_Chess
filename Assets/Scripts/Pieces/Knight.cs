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
            BoardTile tile = Board.Instance.GetTileAt(possibleMove);
            Piece piece = Board.Instance.GetPieceAt(possibleMove);
            if (tile && (!piece || !TurnManager.Instance.IsCurrentPlayerPiece(piece)))
                results.Add(possibleMove);
        }
        return results;
    }
}
