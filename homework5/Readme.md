# Readme
## 视频链接：
[http://v.youku.com/v_show/id_XMzU2MjMwMTUwOA==.html](http://v.youku.com/v_show/id_XMzU2MjMwMTUwOA==.html)
## 在本次增加了以下四个文件
### PsyActionManger.cs
	using UnityEngine;
	using System.Collections;

	public class PsyActionManger : SSActionManager
	{
	    public PsyFlyAction fly;
	    public IActionSelector scene_controller;
	
	    void Start()
	    {
	        this.scene_controller = Singleton<IActionSelector>.Instance;
	        //scene_controller = SSDirector.getInstance().currentSceneController as IActionSelector;
	        scene_controller.psy_Manager = this;
	    }
	    public void PsyFly(GameObject disk, float power)
	    {
	        fly = PsyFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, power);
	        RunAction(disk, fly, (ISSActionCallback)this);
	    }
	}

### PsyFlyAction.cs
	using UnityEngine;
	using System.Collections;
	
	public class PsyFlyAction : SSAction
	{
	    private Vector3 start_vector;
	    private float time;
	    public float power;
	    private Vector3 current_angle = Vector3.zero;
	
	    private PsyFlyAction() {}
	
	    public static PsyFlyAction GetSSAction(Vector3 direction, float power)
	    {
	        PsyFlyAction action = CreateInstance<PsyFlyAction>();
	        if (direction.x == -1)
	        {
	            action.start_vector = Quaternion.Euler(new Vector3(0, 0, -20)) * Vector3.left * power;
	        }
	        else
	        {
	            action.start_vector = Quaternion.Euler(new Vector3(0, 0, 20)) * Vector3.right * power;
	        }
	        action.power = power;
	        return action;
	    }
	
	    public override void Update() { }
	
	    public override void FixedUpdate()
	    {
	        if (transform.position.y < -10)
	        {
	            destroy = true;
	            callback.SSActionEvent(this);
	        }
	    }
	
	    public override void Start()
	    {
	        gameobject.GetComponent<Rigidbody>().velocity = power / 5 * start_vector;
	        gameobject.GetComponent<Rigidbody>().useGravity = true;
	    }
	
	}


### IActionSelector.cs
	using UnityEngine;
	using System.Collections;
	
	public class IActionSelector : MonoBehaviour,IActionManger
	{
	    public CCActionManager cc_Manager;
	    public PsyActionManger psy_Manager;
	
	    // Use this for initialization
	    void Start()
	    {
	        cc_Manager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	
	    }
	
	    public void PlayDisk(GameObject disk, float power, bool usePhy)
	    {
	        if(usePhy == false)
	        {
	            cc_Manager.CCFly(disk, power);
	        }
	        else
	        {
	            psy_Manager.PsyFly(disk, power);
	        }
	    }
	}


### IActionManger.cs
	using UnityEngine;
	using System.Collections;
	public interface IActionManger
	{
	    void PlayDisk(GameObject disk, float power, bool usePhy);
	}