using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Board board;
    public List<List<Piece>> playersPieces = new ();
    public int currentPlayer;

    private void Awake()
    {
        playersPieces.Add(new ());
        playersPieces.Add(new ());
    }

    public void Move (Piece piece, Vector2Int endPos) {
        Debug.Log($"move {piece} to {endPos}");

        /*
        if (!playersPieces[currentPlayer].Contains (piece))
            return;
        */

        if (board.Move (piece.Position, endPos))
            NextPlayer();
    }

    void NextPlayer () {
        currentPlayer++;
        currentPlayer %= playersPieces.Count;
    }
}
