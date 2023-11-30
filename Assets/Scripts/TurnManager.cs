using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Board board;
    public List<List<Piece>> playersPieces = new ();
    public int currentPlayer;

    public void Move (Piece piece, Vector2Int endPos) {
        if (!playersPieces[currentPlayer].Contains (piece))
            return;

        if (board.Move (piece.position, endPos))
            NextPlayer();
    }

    void NextPlayer () {
        currentPlayer++;
        currentPlayer %= playersPieces.Count;
    }
}
