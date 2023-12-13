using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Piece : MonoBehaviour
{
    [SerializeField] Renderer r;

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
            transform.position = new Vector3(position.x, 0f, position.y) + new Vector3(.5f, 0f, .5f);
        }
    }

    Direction direction;
    public Direction PieceDirection {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            switch (direction)
            {
                case Direction.Up:
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    break;
                case Direction.Right:
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                    break;
                case Direction.Down:
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                    break;
                case Direction.Left:
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
                    break;
            }
        }
    }

    public Color PieceColor {
        set
        {
            r.material.color = value;
        }
    }

    public abstract bool IsValidMove (Vector2Int position);
}
