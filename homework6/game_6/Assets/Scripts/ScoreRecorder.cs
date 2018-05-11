using UnityEngine;
using System.Collections;

public class ScoreRecorder : MonoBehaviour
{
    private int score;

    public int getScore()
    {
        return score;
    }

    public void addScore()
    {
        score++;
    }

    public void resetScore()
    {
        score = 0;
    }

    void Start()
    {
        resetScore();
    }
}
