using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Piece : MonoBehaviour
{
    Vector2Int position;
    public Vector2Int Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            transform.position = new Vector3 (position.x, 0f, position.y) + new Vector3 (.5f, 0f, .5f);
        }
    }
    public abstract bool IsValidMove (Vector2Int position);
}
