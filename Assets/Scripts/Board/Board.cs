using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents chess board, contains all the logic for moving pieces and translating between world and board positions
/// </summary>
public class Board : MonoBehaviour
{
    // logical array representing chess board
    [SerializeField] private Piece[,] board;
    // grid used for translating between world and board positions
    [SerializeField] private Grid grid;
    
    // references to transforms used for keeping the hierarchy clean
    [SerializeField] private Transform modelParent;
    [SerializeField] private Transform piecesParent;

    private Vector2Int boardSize;

    /// <summary>
    /// Initialize new board with given size
    /// </summary>
    public void CreateBoard(Vector2Int size)
    {
        board = new Piece[size.x, size.y];
        boardSize = size;
    }

    /// <summary>
    /// Get transform of the parent of all the board models, used for keeping the hierarchy clean
    /// </summary>
    public Transform GetModelParent()
    {
        return modelParent;
    }
    
    /// <summary>
    /// Get transform of the parent of all the pieces prefabs, used for keeping the hierarchy clean
    /// </summary>
    public Transform GetPiecesParent()
    {
        return piecesParent;
    }

    /// <summary>
    /// Add new piece to the board, piece will be placed on the given position
    /// </summary>
    /// <param name="piece">Piece that will be added</param>
    /// <param name="position">Position on board</param>
    public void AddPiece(Piece piece, Vector2Int position)
    {
        board[position.x, position.y] = piece;
        piece.Position = position;
    }

    /// <summary>
    /// Move piece from startPos to endPos, returns true if move was successful
    /// </summary>
    /// <param name="startPos">move piece from this position</param>
    /// <param name="endPos">to this position</param>
    /// <returns>true if move was successful</returns>
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

    /// <summary>
    /// Get position on board using world position
    /// </summary>
    /// <param name="worldPosition">Absolute world position to translate</param>
    /// <returns>Vector2Int position on board</returns>
    public Vector2Int WorldToBoardPosition (Vector3 worldPosition)
    {
        return (Vector2Int)grid.WorldToCell(worldPosition);
    }
    
    /// <summary>
    /// Get absolute world position from board position
    /// </summary>
    /// <param name="boardPosition">Position on board to translate</param>
    /// <returns>Vector3 absolute world position</returns>
    public Vector3 BoardToWorldPosition (Vector2Int boardPosition)
    {
        return grid.CellToWorld((Vector3Int)boardPosition);
    }

    /// <summary>
    /// Get piece on board at given position
    /// </summary>
    /// <param name="position">Absolute world position</param>
    /// <returns>Piece on board at absolute world position</returns>
    public Piece GetPieceAt(Vector3 position)
    {
        Vector3Int gridPos = grid.WorldToCell(position);
        if (gridPos.x < 0 || gridPos.x >= boardSize.x || gridPos.y < 0 || gridPos.y >= boardSize.y)
            return null;

        return board[gridPos.x, gridPos.y];
    }
}
