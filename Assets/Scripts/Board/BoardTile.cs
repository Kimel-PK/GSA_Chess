using UnityEngine;

/// <summary>
/// Represents single board tile
/// </summary>
public class BoardTile : MonoBehaviour, IHighlightable
{
    [SerializeField] private Renderer r;

    private Color baseColor;
    private bool selected;
    private Color selectedColor;
    private bool highlighted;
    private Color highlightedColor;
    
    private Color color;
    private Color Color
    {
        set
        {
            color = value;
            r.material.color = color;
        }
    }

    /// <summary>
    /// Temporarily change board tile color
    /// </summary>
    /// <param name="highlightColor">Temporary tile color</param>
    public void Highlight(Color highlightColor)
    {
        highlighted = true;
        highlightedColor = highlightColor;
        Color = highlightedColor;
    }

    public void Unhighlight()
    {
        highlighted = false;
        if (selected)
        {
            Color = selectedColor;
            return;
        }

        Color = baseColor;
    }

    /// <summary>
    /// Temporarily change board tile color
    /// </summary>
    /// <param name="selectColor"></param>
    public void Select(Color selectColor)
    {
        selected = true;
        selectedColor = selectColor;
        if (highlighted)
            return;
        
        Color = selectedColor;
    }

    public void Deselect()
    {
        selected = false;
        if (highlighted)
        {
            Color = highlightedColor;
            return;
        }

        Color = baseColor;
    }

    /// <summary>
    /// Set board tile color
    /// </summary>
    /// <param name="newColor">New board tile color</param>
    public void SetColor (Color newColor)
    {
        baseColor = newColor;
        Color = newColor;
    }
}
