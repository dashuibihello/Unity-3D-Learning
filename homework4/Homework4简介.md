# 编写一个简单的鼠标打飞碟（Hit UFO）游戏

## 视频地址
[http://v.youku.com/v_show/id_XMzU0NTcwNDUyOA==.html?firsttime=29](http://v.youku.com/v_show/id_XMzU0NTcwNDUyOA==.html?firsttime=29)
ps: 视频中物体大小不会变化，但在录制视频后更改，现在大小会不同

## 游戏内容要求：
1. 游戏有 n 个 round，每个 round 都包括10 次 trial；
2. 每个 trial 的飞碟的色彩、大小、发射位置、速度、角度、同时出现的个数都可能不同。它们由该 round 的 ruler 控制；
3. 每个 trial 的飞碟有随机性，总体难度随 round 上升；
4. 鼠标点中得分，得分规则按色彩、大小、速度不同计算，规则可自由设定。


## 游戏的要求：
1. 使用带缓存的工厂模式管理不同飞碟的生产与回收，该工厂必须是场景单实例的！具体实现见参考资源 Singleton 模板类
2. 近可能使用前面 MVC 结构实现人机交互与游戏模型分离


## 游戏规则
1. 每轮十个球，分十次发射，每一轮需要得到16 * round的分数即可过关（一轮满分26）
2. 球详细信息
	* 黄色球1分，大小为标准大小2倍，一轮4个
	* 绿色球2分，大小为标准大小1.5倍，一轮3个
	* 黑色球4分，大小为标准大小1.2倍，一轮2个
	* 红色球8分，大小为标准大小1倍，一轮1个
3. 每一轮速度加一

## 贴几个具体实现的类

### DiskFactory
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class DiskFactory : MonoBehaviour
	{
    public GameObject diskPrefab;
    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();
	
	//将十个球载入工厂
    void Start()
    {
        GameObject newDisk = null;
        float RanX;
        for (int i = 0; i < 4; i++)
        {
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
            newDisk.GetComponent<Renderer>().material.color = Color.yellow;
            newDisk.GetComponent<DiskData>().color = Color.yellow;
            newDisk.GetComponent<DiskData>().speed = 1f;
            RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
            newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
            newDisk.transform.localScale = new Vector3(2, 2, 2);
            newDisk.SetActive(false);
            free.Add(newDisk.GetComponent<DiskData>());
        }
        for (int i = 0; i < 3; i++)
        {
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
            newDisk.GetComponent<DiskData>().color = Color.green;
            newDisk.GetComponent<DiskData>().speed = 2f;
            RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
            newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
            newDisk.GetComponent<Renderer>().material.color = Color.green;
            newDisk.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            newDisk.SetActive(false);
            free.Add(newDisk.GetComponent<DiskData>());
        }
        for (int i = 0; i < 2; i++)
        {
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
            newDisk.GetComponent<DiskData>().color = Color.black;
            newDisk.GetComponent<DiskData>().speed = 4f;
            RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
            newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
            newDisk.GetComponent<Renderer>().material.color = Color.black;
            newDisk.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            newDisk.SetActive(false);
            free.Add(newDisk.GetComponent<DiskData>());
        }
        newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
        newDisk.AddComponent<DiskData>();
        newDisk.GetComponent<DiskData>().color = Color.red;
        newDisk.GetComponent<DiskData>().speed = 8f;
        RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
        newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
        newDisk.GetComponent<Renderer>().material.color = Color.red;
        newDisk.transform.localScale = new Vector3(1, 1, 1);
        newDisk.SetActive(false);
        free.Add(newDisk.GetComponent<DiskData>());
    }

    public GameObject GetDisk(int round)
    {
        GameObject newDisk = null;
        int getRandom = Random.Range(0, free.Count);
        if (free.Count > 0)
        {
            newDisk = free[getRandom].gameObject;
            free.Remove(free[getRandom]);
        }
        newDisk.GetComponent<DiskData>().speed += 1f;
        used.Add(newDisk.GetComponent<DiskData>());
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }

    public void FreeDisk(GameObject disk)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
	}


### DiskData
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;


	public class DiskData : MonoBehaviour
	{                             
    public Color color;                   
    public Vector3 direction;
    public float speed;
    public Dictionary<Color, int> scoreDictionary = new Dictionary<Color, int>();

	//提前将每个颜色对应分数录入
    void Start()
    {
        scoreDictionary.Add(Color.yellow, 1);
        scoreDictionary.Add(Color.green, 2);
        scoreDictionary.Add(Color.black, 4);
        scoreDictionary.Add(Color.red, 8);
    }
	}

### FirstController
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor.SceneManagement;
	using UnityEngine;

	public class FirstController : MonoBehaviour, IUserAction, ISceneController
	{
    public enum State { PLAYING, REST, STOP, COUNTDOWN, LOSE };
    public State state = State.STOP;
    public float countDown = 3f;

    public CCActionManager action_manager;
    public ScoreRecorder scoreRecorder;
    public DiskFactory diskFactory;
    private List<GameObject> freeDisk = new List<GameObject>();
    public Queue<GameObject> diskQueue = new Queue<GameObject>();

    int round = 0;


    void Awake()
    {
        state = State.STOP;
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
    }

    void Start()
    {

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
            action_manager.CCFly(disk, disk.GetComponent<DiskData>().speed);
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


