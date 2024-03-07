using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public BoardDataScriptableObject boardData;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        StartCoroutine(EStartGame());
    }

    IEnumerator EStartGame()
    {
        var task = SceneManager.LoadSceneAsync(1);
        yield return new WaitUntil(() => task.isDone);
        BoardGenerator.Instance.GenerateBoard(boardData);
    }
}
