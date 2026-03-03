using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "StageData")]
public class StageData : ScriptableObject
{
    public List<GameObject> stagePrefabs;

    public void RandomizeStage()
    {
        for (var i = stagePrefabs.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (stagePrefabs[i], stagePrefabs[j]) = (stagePrefabs[j], stagePrefabs[i]);
        }
    }
}