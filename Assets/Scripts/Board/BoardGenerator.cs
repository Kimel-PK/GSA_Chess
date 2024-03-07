using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates board from BoardDataScriptableObject
/// </summary>
public class BoardGenerator : MonoBehaviour
{
    public static BoardGenerator Instance;

    // references to other scripts in scene
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private Board board;
    
    // prefabs used in board generation
    [SerializeField] private GameObject boardTilePrefab;
    [SerializeField] private GameObject[] boardSidesPrefabs;
    [SerializeField] private GameObject[] boardCornersPrefabs;

    // prefabs used in pieces generation
    [SerializeField] private GameObject[] piecesPrefabs;
    
    // colors used in board generation
    [SerializeField] private Color whiteTileColor;
    [SerializeField] private Color blackTileColor;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Generates board based on data from BoardDataScriptableObject
    /// </summary>
    public void GenerateBoard (BoardDataScriptableObject boardData)
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

                Piece piece = null;
                
                // instantiate new piece and set its color and direction
                if (spawnData.boardTileType != BoardTileType.Empty)
                {
                    GameObject go = Instantiate(piecesPrefabs[(int)spawnData.boardTileType], board.GetPiecesParent());
                    piece = go.GetComponent<Piece>();
                    piece.PieceColor = spawnData.playerNumber == 0 ? Color.white : Color.black;
                    piece.PieceDirection = spawnData.direction;
                }

                // add new piece to game logic
                board.AddTile(boardTile, piece, new Vector2Int(x, z));
                turnManager.AddPiece(piece, spawnData.playerNumber);
            }
        }
        
        // generate board edges
        // TODO not every case is tested yet
        for (int i = -1; i <= board.Size.x; i++)
        {
            for (int j = -1; j <= board.Size.y; j++)
            {
                // edges are generated only if there is no tile on the given position
                if (board.IsTileAt (new Vector2Int(i, j)))
                    continue;
                
                // use binary operations to determine which edges and corners should be instantiated
                
                byte sidesToInstantiate = board.AdjacentTiles(new Vector2Int(i, j));
                if (sidesToInstantiate != 0)
                    Instantiate(boardSidesPrefabs[sidesToInstantiate], board.BoardToWorldPosition(new Vector2Int(i, j)), Quaternion.identity, board.GetModelParent());

                sidesToInstantiate = (byte)((sidesToInstantiate << 1) | sidesToInstantiate);
                if (sidesToInstantiate > 15)
                    sidesToInstantiate = 15;
                
                byte cornersToInstantiate = board.AdjacentCorners(new Vector2Int(i, j));

                cornersToInstantiate = (byte)(cornersToInstantiate & ~sidesToInstantiate);
                for (int k = 0; k < 4; k++)
                {
                    if ((cornersToInstantiate & (1 << k)) != 0)
                        Instantiate(boardCornersPrefabs[k], board.BoardToWorldPosition(new Vector2Int(i, j)), Quaternion.identity, board.GetModelParent());
                }
            }
        }
    }
}