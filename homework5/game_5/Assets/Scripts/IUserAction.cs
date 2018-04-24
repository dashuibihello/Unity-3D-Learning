using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Startgame();
    void Restart();
    int getScore();
    int getRound();
    void usePsy();
    void Click(Vector3 pos);
}