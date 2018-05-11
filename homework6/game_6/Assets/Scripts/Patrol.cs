using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour, IHandle
{
    private int currentSide;
    private int sideNum;
    private int count;
    private FirstController myobj;

    private Vector3[] posSet;
    private Vector3 oldpos;
    private Vector3 playerPos;

    private float field = 3f;
    private float MoveSpeed;
    private bool isCatching;
    private bool playerIsLive = true;
    private bool collision = false;

    public delegate void getScore();
    public event getScore escape;

    public void register(getScore score)
    {
        escape += score;
    }

    public void unRegister(getScore score)
    {
        escape -= score;
    }



    void Start()
    {
        myobj = SSDirector.getInstance().currentSceneController as FirstController;
        currentSide = 0;
        count = 0;
        sideNum = Random.Range(3, 6);
        currentSide = 0;
        oldpos = transform.position;
        MoveSpeed = 1;
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
        playerIsLive = isLive;
        playerPos = pos;
    }

    public void catchPlayer()
    {
        isCatching = true;
        transform.LookAt(playerPos);
        transform.position = Vector3.Lerp(transform.position, playerPos, 1.5f * Time.deltaTime);
    }

    public void getscore()
    {
        if (isCatching && playerIsLive)
        {
            isCatching = false;
            if (escape != null)
            {
                escape();
            }
        }

    }

    public bool outofField()
    {
        if(transform.position.x - oldpos.x > 3 || transform.position.x - oldpos.x < -3)
        {
            return true;
        }
        if (transform.position.z - oldpos.z > 3 || transform.position.z - oldpos.z < -3)
        {
            return true;
        }
        return false;
    }

    public bool inField(Vector3 targetPos)
    {
        float distance = (transform.position - targetPos).sqrMagnitude;
        if (distance <= field * field)
        {
            return true;
        }
        return false;
    }

    void patrolmove()
    {
        if(!collision && !outofField())
        {
            count++;
            transform.Translate(posSet[currentSide] * MoveSpeed * Time.deltaTime);
            if (count == 60)
            {
                count = 0;
                currentSide = (++currentSide) % sideNum;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, oldpos, MoveSpeed * Time.deltaTime);
            count = 0;
            currentSide = 0;
            if(Vector3.Distance(oldpos, transform.position) < 0.3f)
            {
                collision = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Map")
        {
            collision = true;
        }

    }


    void Update()
    {
        if(myobj.GameStart())
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (playerIsLive && inField(playerPos))
            {
                catchPlayer();
            }
            else
            {
                if (isCatching)
                {
                    getscore();
                }
                patrolmove();
            }
        }     
    }
}