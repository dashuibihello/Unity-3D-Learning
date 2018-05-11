using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour
{
    private float[] posx = { 1, 1, 1, -6, -6, -6 };
    private float[] posz = { -5, -1, 3, 3, -1, -5 };
    private int count;

    void Start()
    {
        count = 0;
    }

    public GameObject getPatrol()
    {
        GameObject patrol = Instantiate(Resources.Load<GameObject>("prefabs/Patrol"), new Vector3(posx[count], 0, posz[count]), Quaternion.identity);
        count++;
        return patrol;
    }

    public void freePatrol(GameObject p)
    {
        p.SetActive(false);
    }
}

