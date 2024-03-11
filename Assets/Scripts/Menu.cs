using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] List<BoardDataScriptableObject> boardDataList;

    private void Start()
    {
        GameManager.Instance.boardData = boardDataList[0];
    }

    public void Play()
    {
        GameManager.Instance.StartGame();
    }

    public void OnBoardSelect (int index)
    {
        GameManager.Instance.boardData = boardDataList[index];
    }
}
