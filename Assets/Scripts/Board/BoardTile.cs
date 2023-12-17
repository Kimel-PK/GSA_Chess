using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents single board tile
/// </summary>
public class BoardTile : MonoBehaviour
{
    [SerializeField] private Renderer r;
    
    /// <summary>
    /// Set board tile color
    /// </summary>
    /// <param name="color">New board tile color</param>
    public void SetColor (Color color)
    {
        r.material.color = color;
    }
}
