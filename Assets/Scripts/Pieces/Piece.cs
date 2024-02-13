using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour, IHighlightable, ISelectable
{
    // TODO replace with Board singleton
    public Board board;
    
    [SerializeField] private Renderer r;
    [SerializeField] private Outline outline;
    
    private bool selected;
    private Color selectedColor;
    private bool highlighted;
    private Color highlightedColor;

    /// <summary>
    /// Property used for keep track of piece position on the board and synchronizing it with transform position automatically 
    /// </summary>
    private Vector2Int position;
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
    private Direction direction;
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
    /// <param name="movePosition"></param>
    /// <returns>True if move is valid</returns>
    public abstract bool IsValidMove (Vector2Int movePosition);

    public abstract List<Vector2Int> GetValidMoves();

    /// <summary>
    /// Add red outline to piece
    /// </summary>
    /// <param name="highlightColor">Outline color</param>
    public void Highlight(Color highlightColor)
    {
        highlighted = true;
        highlightedColor = highlightColor;
        outline.OutlineColor = highlightedColor;
        outline.enabled = true;
    }
    
    public void Unhighlight()
    {
        highlighted = false;
        if (selected)
        {
            outline.OutlineColor = selectedColor;
            return;
        }
        outline.enabled = false;
    }

    /// <summary>
    /// Add yellow outline to piece
    /// </summary>
    /// <param name="selectColor">Outline color</param>
    public void Select(Color selectColor)
    {
        selected = true;
        selectedColor = selectColor;
        if (highlighted)
            return;
        outline.OutlineColor = selectedColor;
        outline.enabled = true;
    }
    
    public void Deselect()
    {
        selected = false;
        if (highlighted)
            return;
        outline.enabled = false;
    }
}
