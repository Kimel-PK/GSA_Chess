using UnityEngine;

/// <summary>
/// Represents chess board, contains all the logic for moving pieces and translating between world and board positions
/// </summary>
public class Board : MonoBehaviour
{
    public static Board Instance;

    // logical arrays representing chess board
    [SerializeField] private BoardTile[,] board;
    [SerializeField] private Piece[,] pieces;
    // grid used for translating between world and board positions
    [SerializeField] private Grid grid;
    
    // references to transforms used for keeping the hierarchy clean
    [SerializeField] private Transform modelParent;
    [SerializeField] private Transform piecesParent;

    public Vector2Int Size { get; private set; }

    private void Awake ()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Initialize new board with given size
    /// </summary>
    public void CreateBoard(Vector2Int size)
    {
        board = new BoardTile[size.x, size.y];
        pieces = new Piece[size.x, size.y];
        Size = size;
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
    /// Add new tile to the board, piece will be placed on the given position
    /// </summary>
    /// <param name="boardTile">Tile that will be added</param>
    /// <param name="piece">Piece that will be added</param>
    /// <param name="position">Position on board</param>
    public void AddTile(BoardTile boardTile, Piece piece, Vector2Int position)
    {
        board[position.x, position.y] = boardTile;
        if (!piece)
            return;
        
        pieces[position.x, position.y] = piece;
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
        
        Piece piece = pieces[startPos.x, startPos.y];
        if (!piece.IsValidMove(endPos))
            return false;
        
        pieces[startPos.x, startPos.y] = null;
        pieces[endPos.x, endPos.y] = piece;
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
        if (gridPos.x < 0 || gridPos.x >= Size.x || gridPos.y < 0 || gridPos.y >= Size.y)
            return null;

        return pieces[gridPos.x, gridPos.y];
    }

    public Piece GetPieceAt(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= Size.x || gridPos.y < 0 || gridPos.y >= Size.y)
            return null;

        return pieces[gridPos.x, gridPos.y];
    }

    public BoardTile GetTileAt(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= Size.x || gridPos.y < 0 || gridPos.y >= Size.y)
            return null;

        return board[gridPos.x, gridPos.y];
    }

    /// <summary>
    /// Check if there is board tile at given position
    /// </summary>
    /// <param name="position">position to check</param>
    /// <returns>true if there is board tile</returns>
    public bool IsTileAt (Vector2Int position)
    {
        if (position.x < 0 || position.x >= Size.x || position.y < 0 || position.y >= Size.y)
            return false;
        return board[position.x, position.y];
    }

    /// <summary>
    /// Get binary representation of adjacent tiles
    /// </summary>
    /// <param name="position">position where edges will be generated</param>
    /// <returns>byte number representing array of adjacent tiles</returns>
    public byte AdjacentTiles(Vector2Int position)
    {
        byte result = 0;
        if (IsTileAt(new Vector2Int(position.x, position.y + 1)))
            result += 1;
        if (IsTileAt(new Vector2Int(position.x + 1, position.y)))
            result += 2;
        if (IsTileAt(new Vector2Int(position.x, position.y - 1)))
            result += 4;
        if (IsTileAt(new Vector2Int(position.x - 1, position.y)))
            result += 8;

        return result;
    }
    
    /// <summary>
    /// Get binary representation of corner tiles
    /// </summary>
    /// <param name="position">position where edges will be generated</param>
    /// <returns>byte number representing array of corner tiles</returns>
    public byte AdjacentCorners(Vector2Int position)
    {
        byte result = 0;
        if (IsTileAt(new Vector2Int(position.x - 1, position.y + 1)))
            result += 1;
        if (IsTileAt(new Vector2Int(position.x + 1, position.y + 1)))
            result += 2;
        if (IsTileAt(new Vector2Int(position.x + 1, position.y - 1)))
            result += 4;
        if (IsTileAt(new Vector2Int(position.x - 1, position.y - 1)))
            result += 8;

        return result;
    }
}
