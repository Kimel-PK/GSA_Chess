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
        set
        {
            color = value;
            r.material.color = color;
        }
    }

    /// <summary>
    /// Temporarily change board tile color to red
    /// </summary>
    /// <param name="state">True to enable highlight, false to disable</param>
    public void Highlight(bool state)
    {
        Color = state ? Color.red : baseColor;
    }

    /// <summary>
    /// Temporarily change board tile color to yellow
    /// </summary>
    /// <param name="state">True to enable highlight, false to disable</param>
    public void Select(bool state)
    {
        Color = state ? Color.yellow : baseColor;
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
