using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Piece[,] board = new Piece[8,8];

    public bool Move (Vector2Int startPos, Vector2Int endPos) {
        if (!board[startPos.x, startPos.y])
            return false;
        
        Piece piece = board[startPos.x, startPos.y];
        if (!piece.IsValidMove(endPos))
            return false;
        
        board[startPos.x, startPos.y] = null;
        board[endPos.x, endPos.y] = piece;
        piece.position = endPos;
        return true;
    }

    
}
