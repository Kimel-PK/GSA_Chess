using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Piece[,] board = new Piece[8,8];
    [SerializeField] Grid grid;

    [SerializeField] GameObject pawnPrefab;

    public void AddPiece(Piece piece, Vector2Int position)
    {
        board[position.x, position.y] = piece;
        piece.Position = position;
    }

    public bool Move (Vector2Int startPos, Vector2Int endPos) {
        if (!board[startPos.x, startPos.y])
            return false;
        
        Piece piece = board[startPos.x, startPos.y];
        if (!piece.IsValidMove(endPos))
            return false;
        
        board[startPos.x, startPos.y] = null;
        board[endPos.x, endPos.y] = piece;
        piece.Position = endPos;
        return true;
    }

    public Vector2Int WorldToBoardPosition (Vector3 worldPosition)
    {
        return (Vector2Int)grid.WorldToCell(worldPosition);
    }

    public Piece GetPieceAt(Vector3 position)
    {
        Vector3Int gridPos = grid.WorldToCell(position);
        if (gridPos.x < 0 || gridPos.x >= 8 || gridPos.y < 0 || gridPos.y >= 8)
            return null;

        return board[gridPos.x, gridPos.y];
    }
}
