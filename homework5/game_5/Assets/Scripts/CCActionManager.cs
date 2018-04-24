using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager
{

    public CCFlyAction fly;                            
    public IActionSelector scene_controller;             

    void Start()
    {
        scene_controller = Singleton<IActionSelector>.Instance;
        scene_controller.cc_Manager = this;
    }
    public void CCFly(GameObject disk, float power)
    {
        fly = CCFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, power);
        RunAction(disk, fly, (ISSActionCallback)this);
    }
}