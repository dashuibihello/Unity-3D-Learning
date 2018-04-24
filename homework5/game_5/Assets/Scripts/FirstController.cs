using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FirstController : MonoBehaviour, IUserAction, ISceneController
{
    public enum State { PLAYING, REST, STOP, COUNTDOWN, LOSE };
    public State state = State.STOP;
    public float countDown = 3f;

    public IActionSelector action_manager;
    public ScoreRecorder scoreRecorder;
    public DiskFactory diskFactory;
    private List<GameObject> freeDisk = new List<GameObject>();
    public Queue<GameObject> diskQueue = new Queue<GameObject>();

    int round = 0;
    public bool isUsePsy = false;


    void Awake()
    {
        state = State.STOP;
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        action_manager = Singleton<IActionSelector>.Instance;
        diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
    }

    void Start()
    {

    }

    public void usePsy()
    {
        isUsePsy = true;
    }

    void ThrowDisk()
    {
        float position_x = 10;
        if (diskQueue.Count != 0)
        {
            GameObject disk = diskQueue.Dequeue();
            freeDisk.Add(disk);
            disk.SetActive(true);
            float ran_y = Random.Range(1f, 3.5f);
            float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
            disk.GetComponent<DiskData>().direction = new Vector3(ran_x, ran_y, 0);
            Vector3 position = new Vector3(-disk.GetComponent<DiskData>().direction.x * position_x, ran_y, 0);
            disk.transform.position = position;
            action_manager.PlayDisk(disk, disk.GetComponent<DiskData>().speed, isUsePsy);
        }
    }

    public void Countdown()
    {
        if (countDown > 0 && state == State.COUNTDOWN)
        {
            countDown -= Time.deltaTime;
        } else
        {
            state = State.PLAYING;
        }
    }

    public float getCountdown()
    {
        return countDown;
    }

    public void Startgame()
    {
        round++;
        state = State.COUNTDOWN;
        NextRound();
    }

    public void Restart()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }

    public int getScore()
    {
        return scoreRecorder.score;
    }

    public int getRound()
    {
        return round;
    }

    public void Click(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<DiskData>() != null)
            {
                for (int j = 0; j < freeDisk.Count; j++)
                {
                    if (hit.collider.gameObject.GetInstanceID() == freeDisk[j].gameObject.GetInstanceID())
                    {
                        scoreRecorder.Record(hit.collider.gameObject);
                        StartCoroutine(WaitingParticle(0.08f, hit, diskFactory, hit.collider.gameObject));
                        freeDisk.Remove(freeDisk[j]);
                        break;
                    }
                }
            }

        }
    }

    void NextRound()
    {
        DiskFactory newfactory = Singleton<DiskFactory>.Instance;
        for (int i = 0; i < 10; i++)
        {
            diskQueue.Enqueue(newfactory.GetDisk(round));
        }
    }

    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject obj)
    {
        yield return new WaitForSeconds(wait_time);
        hit.collider.gameObject.transform.position = new Vector3(0, -100, 0);
        disk_factory.FreeDisk(obj);
    }

    void Update()
    {
        if(state == State.REST || state == State.STOP)
        {
            if (scoreRecorder.score <= round * 16 && state == State.REST)
                state = State.LOSE;
            countDown = 3f;
        }
        else if(state == State.COUNTDOWN)
        {
            Countdown();
        }
        else if(state == State.PLAYING)
        {
            for (int i = 0; i < freeDisk.Count; i++)
            {
                GameObject temp = freeDisk[i];
                if (temp.transform.position.y < -10 && temp.gameObject.activeSelf == true)
                {
                    diskFactory.FreeDisk(freeDisk[i]);
                    freeDisk.Remove(freeDisk[i]);
                }
            }
            if(freeDisk.Count == 0)
                ThrowDisk();
            if (diskQueue.Count == 0 && freeDisk.Count == 0)
                state = State.REST;
        }
    }
}


