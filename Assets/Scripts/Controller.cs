using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player input
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private Board board;
    
    private Vector2 mousePosition;
    private IHighlightable highlightedObject;
    
    private Piece selectedPiece;
    private Piece SelectedPiece {
        get {
            return selectedPiece;
        }
        set {
            if (selectedPiece == value)
                return;

            if (selectedPiece)
                selectedPiece.Deselect();
            
            selectedPiece = value;
            if (!selectedPiece)
                return;
            
            selectedPiece.Select(Color.red);
            foreach (Vector2Int tilePosition in selectedPiece.GetValidMoves()) {
                board.GetTileAt(tilePosition).Select(Color.red);
            }
        }
    }

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

        // TODO convert to singleton
        Camera mainCamera = Camera.main;
        
        // using tracked mouse position send a raycast towards the board
        Debug.DrawRay(mainCamera.ScreenPointToRay(mousePosition).origin, mainCamera.ScreenPointToRay(mousePosition).direction * 100f, Color.red, 1f);
        // if raycast doesn't hit anything end the function
        if (!Physics.Raycast(mainCamera.ScreenPointToRay(mousePosition), out RaycastHit rHit, 100f))
            return;
        
        // try to get piece at the hit position
        Piece piece = board.GetPieceAt(rHit.point);
        
        // if there is no selectedPiece and raycast actually hit something, select that piece
        if (!SelectedPiece && piece)
        {
            SelectedPiece = piece;
            return;
        }
        
        // if there is no selectedPiece after this, end the function
        if (!SelectedPiece)
            return;
        
        // we selected something and clicked on board so try to move selected piece to the clicked position
        foreach (Vector2Int tilePosition in selectedPiece.GetValidMoves()) {
            board.GetTileAt(tilePosition).Deselect();
        }
        turnManager.Move(SelectedPiece, board.WorldToBoardPosition(rHit.point));
        // after move we want to deselect piece
        SelectedPiece = null;
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
        
        // TODO convert to singleton
        Camera mainCamera = Camera.main;

        // using tracked mouse position send a raycast towards the board
        Debug.DrawRay(mainCamera.ScreenPointToRay(mousePosition).origin, mainCamera.ScreenPointToRay(mousePosition).direction * 100f, Color.red, 1f);
        
        if (!Physics.Raycast(mainCamera.ScreenPointToRay(mousePosition), out RaycastHit rHit, 100f))
        {
            highlightedObject?.Unhighlight();
            highlightedObject = null;
            return;
        }

        IHighlightable hoveredObject = rHit.collider.GetComponentInParent<IHighlightable>();
        
        if (hoveredObject == highlightedObject)
            return;

        highlightedObject?.Unhighlight();
        highlightedObject = hoveredObject;
        highlightedObject?.Highlight(Color.yellow);
    }

    /// <summary>
    /// Deselect piece on right click
    /// </summary>
    /// <param name="context"></param>
    public void RightClick (InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        foreach (Vector2Int tilePosition in SelectedPiece.GetValidMoves()) {
            board.GetTileAt(tilePosition).Deselect();
        }
        SelectedPiece = null;
    }
}
