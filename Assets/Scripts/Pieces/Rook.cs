using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override bool IsValidMove (Vector2Int position) {
        return true;
    }
}
