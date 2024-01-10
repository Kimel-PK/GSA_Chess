using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents single board tile
/// </summary>
public class BoardTile : MonoBehaviour, IHighlightable, ISelectable
{
    [SerializeField] private Renderer r;

    private Color baseColor;
    private Color color;
    private Color Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
            r.material.color = color;
        }
    }

    public void Highlight(bool state)
    {
        Color = state ? Color.red : baseColor;
    }

    public void Select(bool state)
    {
        Color = state ? Color.yellow : baseColor;
    }

    /// <summary>
    /// Set board tile color
    /// </summary>
    /// <param name="color">New board tile color</param>
    public void SetColor (Color color)
    {
        baseColor = color;
        Color = color;
    }
}
