using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    void Start()
    {
        score = 0;
    }

    public void Record(GameObject disk)
    {
        score += disk.GetComponent<DiskData>().scoreDictionary[disk.GetComponent<DiskData>().color];
    }

    public void Reset()
    {
        score = 0;
    }
}
