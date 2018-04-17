using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Startgame();
    void Restart();
    int getScore();
    int getRound();
    void Click(Vector3 pos);
}