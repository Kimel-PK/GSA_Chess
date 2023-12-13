using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/BoardDataScriptableObject", order = 1)]
public class BoardDataScriptableObject : ScriptableObject
{
    public List<SpawnData> piecesData;
}