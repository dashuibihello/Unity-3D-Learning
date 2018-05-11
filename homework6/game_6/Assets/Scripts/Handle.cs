using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandle
{
    void Reaction(bool isLive, Vector3 pos);
}