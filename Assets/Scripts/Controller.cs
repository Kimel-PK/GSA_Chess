using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] Board board;
    Vector2 mousePosition;
    Piece selectedPiece;

    public void Click (InputAction.CallbackContext context) {
        if (!context.performed)
            return;

        Debug.DrawRay(Camera.main.ScreenPointToRay(mousePosition).origin, Camera.main.ScreenPointToRay(mousePosition).direction * 100f, Color.red, 1f);
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit rHit, 100f))
            return;

        Piece piece = board.GetPieceAt(rHit.point);

        if (!selectedPiece && piece)
        {
            selectedPiece = piece;
            return;
        } else
        {
            turnManager.Move(selectedPiece, board.WorldToBoardPosition(rHit.point));
        }
    }

    public void Hover (InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        mousePosition = context.ReadValue<Vector2>();
    }


}
