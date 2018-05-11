using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour, IHandle
{
    private int currentSide;
    private int sideNum;
    private int count;

    private Vector3[] posSet;
    private Vector3 oldpos;
    private Vector3 playerPos;

    private float field = 3f;
    private float MoveSpeed;
    private bool isCatching;
    private bool playerIsLive = true;

    void Awake()
    {
        currentSide = 0;
        count = 0;
        sideNum = Random.Range(3, 6);
        currentSide = 0;
        oldpos = transform.position;
        MoveSpeed = 1.1f;
        isCatching = false;
        if(sideNum == 3)
        {
            posSet = new Vector3[] { new Vector3 (2, 0, 0), new Vector3 (-1, 0, 2),
                new Vector3 (-1, 0, -2) };
        }
        else if(sideNum == 4)
        {
            posSet = new Vector3[] { new Vector3 (2, 0, 0), new Vector3 (0, 0, 2),
                new Vector3 (-2, 0, 0), new Vector3 (0, 0, -2) };
        } 
        else if(sideNum == 5)
        {
            posSet = new Vector3[] { new Vector3 (2, 0, 0), new Vector3 (0, 0, 1),
                new Vector3 (-1, 0, 1), new Vector3 (-1, 0, -1), new Vector3 (0, 0, -1) };
        }
        
    }

    public void Reaction(bool isLive, Vector3 pos)
    {
        Debug.Log(playerIsLive + " " + pos);

        playerIsLive = isLive;
        playerPos = pos;
    }

    public void catchPlayer()
    {
        isCatching = true;
        transform.LookAt(playerPos);
        transform.position = Vector3.Lerp(transform.position, playerPos, 2 * Time.deltaTime);
    }

    public bool inField(Vector3 targetPos)
    {
        Debug.Log(targetPos);
        float distance = (transform.position - targetPos).sqrMagnitude;
        if (distance <= field * field)
        {
            return true;
        }
        return false;
    }

    void patrolmove()
    {
        count++;
        transform.Translate(posSet[currentSide] * MoveSpeed * Time.deltaTime);
        if(count == 60)
        {
            count = 0;
            currentSide = (++currentSide) % sideNum;
        }
    }


    private void FixedUpdate()
    {
        Debug.Log(playerIsLive + " " + inField(playerPos));
        if (playerIsLive && inField(playerPos))
        {
            catchPlayer();
        }
        else
        {
            patrolmove();
        }
        
    }
}