using UnityEngine;
using System.Collections.Generic;

// ScriptableObject is a class that allows us to create assets that can be used as data containers
// [CreateAssetMenu] attribute allows us to create new assets from Unity Editor - from top menu go to Assets/Create/ScriptableObjects/BoardDataScriptableObject
[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/BoardDataScriptableObject", order = 1)]
public class BoardDataScriptableObject : ScriptableObject
{
    public Vector2Int boardSize;
    public List<SpawnData> piecesData;
}