using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Vector2Int position;
    public abstract bool IsValidMove (Vector2Int position);
}
