using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Piece : MonoBehaviour, IHighlightable, ISelectable
{
    [SerializeField] Renderer r;
    [SerializeField] GameObject tempHighlightObject;
    
    /// <summary>
    /// Property used for keep track of piece position on the board and synchronizing it with transform position automatically 
    /// </summary>
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

    /// <summary>
    /// Property used for keep track of piece direction and synchronizing it with transform rotation automatically
    /// </summary>
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

    /// <summary>
    /// Property used for setting piece visual appearance
    /// </summary>
    public Color PieceColor {
        // there is no need for getting piece color because this is only visual so there is no getter
        set
        {
            r.material.color = value;
        }
    }

    /// <summary>
    /// Check if move at given position is valid for this piece, capturing move is treated as valid move, capturing is handled by Board class
    /// </summary>
    /// <param name="position"></param>
    /// <returns>True if move is valid</returns>
    public abstract bool IsValidMove (Vector2Int position);

    public void Highlight(bool state)
    {
        tempHighlightObject.SetActive(state);
    }

    public void Select(bool state)
    {
        throw new NotImplementedException();
    }
}
