using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager
{

    public CCFlyAction fly;                            
    public FirstController scene_controller;             

    void Start()
    {
        scene_controller = SSDirector.getInstance().currentSceneController as FirstController;
        scene_controller.action_manager = this;
    }
    public void CCFly(GameObject disk, float power)
    {
        fly = CCFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, power);
        RunAction(disk, fly, (ISSActionCallback)this);
    }
}