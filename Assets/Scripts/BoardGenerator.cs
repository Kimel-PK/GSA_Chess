using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] Board board;
    [SerializeField] GameObject[] piecesPrefabs;
    [SerializeField] BoardDataScriptableObject boardData;

    private void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard ()
    {
        int x = 0;
        int y = 0;
        foreach (SpawnData spawnData in boardData.piecesData)
        {
            for (int i = 0; i < spawnData.amount; i++)
            {
                if (spawnData.pieceType != PieceType.None)
                {
                    GameObject go = Instantiate(piecesPrefabs[(int)spawnData.pieceType], board.transform);
                    Piece piece = go.GetComponent<Piece>();
                    if (spawnData.playerNumber == 0)
                        piece.PieceColor = Color.white;
                    else
                        piece.PieceColor = Color.black;
                    board.AddPiece(piece, new Vector2Int(x, y));
                    turnManager.AddPiece(piece, spawnData.playerNumber);
                }

                x++;
                if (x >= 8) {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
