﻿using UnityEngine;
using System.Collections;

public class ScoreRecorder : MonoBehaviour, IHandle
{
    private int score;
    private bool playerIsLive;

    public void Reaction(bool isLive, Vector3 pos)
    {
        playerIsLive = isLive;
    }

    public int getScore()
    {
        return score;
    }

    public void addScore(int s)
    {
        if (playerIsLive)
        {
            score++;
        }
    }

    public void resetScore()
    {
        score = 0;
    }

    void Awake()
    {
        playerIsLive = true;
        score = 0;
    }

    void Update()
    {

    }
}
