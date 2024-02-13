using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages turns and pieces
/// </summary>
public class TurnManager : MonoBehaviour
{
    public Board board;
    public int currentPlayer;
    
    private readonly List<List<Piece>> playersPieces = new ();
    
    /// <summary>
    /// Awake is Unity event that is called on MonoBehaviour while object is initialized - before Start method
    /// </summary>
    private void Awake()
    {
        playersPieces.Add(new ());
        playersPieces.Add(new ());
    }

    /// <summary>
    /// Add piece to turn manager logic
    /// </summary>
    /// <param name="piece">Piece to add</param>
    /// <param name="playerNumber">Which player is this piece owner</param>
    public void AddPiece (Piece piece, int playerNumber)
    {
        playersPieces[playerNumber].Add(piece);
    }

    /// <summary>
    /// Try to move piece to the given position, if move was successful change current player
    /// </summary>
    /// <param name="piece">piece to move</param>
    /// <param name="endPos">position where piece will be moved</param>
    public void Move (Piece piece, Vector2Int endPos) {
        if (!playersPieces[currentPlayer].Contains (piece))
            return;

        if (board.Move (piece.Position, endPos))
            NextPlayer();
    }

    /// <summary>
    /// Helper method to switch to next player and go back to first when current player exceeds number of players 
    /// </summary>
    void NextPlayer () {
        currentPlayer++;
        currentPlayer %= playersPieces.Count;
    }
}
