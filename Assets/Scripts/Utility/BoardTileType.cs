/// <summary>
/// Enum that represents all possible board tile types, used in BoardGenerator script
/// </summary>
public enum BoardTileType 
{
    None, // there is no tile
    Empty, // there is tile but no piece on it
    Pawn, // there is tile and pawn on it
    Rook, // etc.
    Knight,
    Bishop,
    Queen,
    King
}
