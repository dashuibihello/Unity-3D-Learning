using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;



public class FirstController : MonoBehaviour, ISceneController, IUserAction, IScore, IHandle
{
    private GameObject player;
    private SSDirector director;
    private ScoreRecorder scoreRecorder;

    private IHandle sc;

    private bool canOperation;
    private bool create;
    private bool isStart;
    private int bearNum;
    private int ellephantNum;
    private Subject sub;
    private Animator ani;
    private Vector3 movement;

    void Awake()
    {
        director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        director.currentSceneController.CreatePatrols();

        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        sc = director.currentSceneController as IHandle;
        sub = player.GetComponent<Player>();
        sub.Attach(sc);
        ani = player.GetComponent<Animator>();
        isStart = false;
        create = false;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void CreatePatrols()
    {
        PatrolFactory patrolFactory = Singleton<PatrolFactory>.Instance;
        for (int i = 0; i < 6; i++)
        {
            
            GameObject patrol = patrolFactory.getPatrol();
            patrol.AddComponent<Patrol>();
            patrol.name = "Patrol" + i;
        }
    }
    
    public void LoadResources()
    {
        GameObject Map = Instantiate(Resources.Load<GameObject>("prefabs/Map"), new Vector3(0, 0, 0), Quaternion.identity);
        player = Instantiate(Resources.Load<GameObject>("prefabs/Player"), new Vector3(2, 0, -6.5f), Quaternion.identity);
        player.AddComponent<Player>();
    }

    public void movePlayer(float h, float v)
    {
        if (canOperation && isStart)
        {
            player.GetComponent<Player>().move(h, v);
            if (h == 0 && v == 0)
            {
                ani.SetTrigger("stop");
            }
            else
            {
                ani.SetTrigger("move");
            }
        }
    }

    public void setDirection(float h, float v)
    {
        if (canOperation && isStart)
        {
            player.GetComponent<Player>().turn(h, v);
        }
    }

    public void StartGame()
    {
        isStart = true;
    }

    public bool GameStart()
    {
        return isStart;
    }

    public void Restart()
    {
        EditorSceneManager.LoadScene("main");
    }

    public bool GameOver()
    {
        return (!canOperation);
    }

    public int getScore()
    {
        return scoreRecorder.getScore();
    }
    
    public void Reaction(bool isLive, Vector3 pos)
    {
        ani.SetBool("live", isLive);
        canOperation = isLive;
    }
    
}
