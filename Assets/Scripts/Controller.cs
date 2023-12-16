using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player input
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private Board board;
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Piece selectedPiece;

    /// <summary>
    /// Select piece on left click
    /// </summary>
    /// <param name="context"></param>
    public void Click (InputAction.CallbackContext context) {
        // new input system action consist of 3 states: started, performed and canceled
        // started is called when button is pressed down
        // performed is called when the action set in InputAction prefab is fired (this action is just a button press)
        // canceled is called when button is released
        if (!context.performed)
            return;
        
        // using tracked mouse position send a raycast towards the board
        Debug.DrawRay(Camera.main.ScreenPointToRay(mousePosition).origin, Camera.main.ScreenPointToRay(mousePosition).direction * 100f, Color.red, 1f);
        // if raycast doesn't hit anything end the function
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit rHit, 100f))
            return;
        
        // try to get piece at the hit position
        Piece piece = board.GetPieceAt(rHit.point);
        
        // if there is no selectedPiece and raycast actually hit something, select that piece
        if (!selectedPiece && piece)
        {
            selectedPiece = piece;
            return;
        }
        
        // if there is no selectedPiece after this, end the function
        if (!selectedPiece)
            return;
        
        // we selected something and clicked on board so try to move selected piece to the clicked position
        turnManager.Move(selectedPiece, board.WorldToBoardPosition(rHit.point));
        // after move we want to deselect piece
        selectedPiece = null;
    }

    /// <summary>
    /// Every time when mouse moves keep track of mouse position on screen
    /// </summary>
    /// <param name="context"></param>
    public void Hover (InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        mousePosition = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Deselect piece on right click
    /// </summary>
    /// <param name="context"></param>
    public void RightClick (InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        selectedPiece = null;
    }
}
