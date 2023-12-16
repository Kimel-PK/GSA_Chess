using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates board from BoardDataScriptableObject
/// </summary>
public class BoardGenerator : MonoBehaviour
{
    // references to other scripts in scene
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private Board board;
    
    // prefabs used in board generation
    [SerializeField] private GameObject boardTilePrefab;
    [SerializeField] private GameObject boardSidePrefab;
    [SerializeField] private GameObject boardCornerPrefab;

    // prefabs used in pieces generation
    [SerializeField] private GameObject[] piecesPrefabs;
    
    // colors used in board generation
    [SerializeField] private Color whiteTileColor;
    [SerializeField] private Color blackTileColor;
    
    // scriptable object with tiles and pieces data, assigned in inspector
    [SerializeField] private BoardDataScriptableObject boardData;
    
    // helper transform used for board edges generation
    // it is easier to use transform for this than calculating positions manually
    [SerializeField] private Transform instantiateHelper;

    /// <summary>
    /// Unity event method called on scene start before first frame
    /// </summary>
    private void Start()
    {
        // on scene start generate board (in future this will be called by game manager)
        GenerateBoard();
    }

    /// <summary>
    /// Generates board based on data from BoardDataScriptableObject
    /// </summary>
    public void GenerateBoard ()
    {
        // initialize new board
        board.CreateBoard(boardData.boardSize);
        
        // because of "continue" statements we increment x at the beginning of the loop, so we start with -1
        int x = -1;
        int z = 0;
        // iterate through all the tiles in boardData
        foreach (SpawnData spawnData in boardData.piecesData)
        {
            for (int i = 0; i < spawnData.amount; i++)
            {
                x++;
                // if we reached the end of the row, go to the next row
                if (x >= boardData.boardSize.x)
                {
                    x = 0;
                    z++;
                }

                // if tile type is None, we don't need to spawn anything (in the future there will be non rectangular boards)
                if (spawnData.boardTileType == BoardTileType.None)
                    continue;

                // instantiate new board tile and set its color
                BoardTile boardTile = Instantiate(boardTilePrefab, board.BoardToWorldPosition(new Vector2Int(x, z)),
                    Quaternion.identity, board.GetModelParent()).GetComponent<BoardTile>();
                boardTile.SetColor((x + z) % 2 == 0 ? whiteTileColor : blackTileColor);
                
                // if tile type is Empty, we don't need to spawn any piece
                if (spawnData.boardTileType == BoardTileType.Empty)
                    continue;
                
                // instantiate new piece and set its color and direction
                GameObject go = Instantiate(piecesPrefabs[(int)spawnData.boardTileType], board.GetPiecesParent());
                Piece piece = go.GetComponent<Piece>();
                piece.PieceColor = spawnData.playerNumber == 0 ? Color.white : Color.black;
                piece.PieceDirection = spawnData.direction;
                
                // add new piece to game logic
                board.AddPiece(piece, new Vector2Int(x, z));
                turnManager.AddPiece(piece, spawnData.playerNumber);
            }
        }
        
        // generate board edges
        // we are going around the board and instantiate corner and side prefabs
        // instantiateHelper track both position and rotation of the next prefab, it is easier to use transform for this than calculating positions manually
        instantiateHelper.transform.position = board.BoardToWorldPosition(new Vector2Int(-1, -1));
        // there are 4 sides of the board, so we need to do this 4 times
        for (int i = 0; i < 4; i++)
        {
            // instantiate corner first, then instantiate sides
            // board is rectangular so we need switch between x and y size
            int currentBoardSize = i % 2 == 0 ? boardData.boardSize.x : boardData.boardSize.y;
            Instantiate(boardCornerPrefab, instantiateHelper.position, instantiateHelper.rotation, board.GetModelParent());
            for (int j = 0; j < currentBoardSize; j++)
            {
                // instantiateHelper.right is rotated towards center of board, so we can use it to calculate position of the next prefab
                instantiateHelper.position += instantiateHelper.right;
                Instantiate(boardSidePrefab, instantiateHelper.position, instantiateHelper.rotation, board.GetModelParent());
            }
            
            // after we finished one side, we need to rotate instantiateHelper to the next side
            instantiateHelper.position += instantiateHelper.right * 2;
            instantiateHelper.Rotate(0, -90, 0);
        }
    }
}