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
