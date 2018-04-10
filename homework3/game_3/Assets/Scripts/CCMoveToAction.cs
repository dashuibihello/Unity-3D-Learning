using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public Vector3 target;
    public float speed;

    private ISSActionCallback monitor = null;

    public void RunAction(Vector3 target, float speed, ISSActionCallback monitor)
    {
        this.target = target;
        this.speed = speed;
        this.monitor = monitor;
        SSDirector.getInstance().setMoving(FirstController.State.MOVING);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (transform.position == target)
        {
            SSDirector.getInstance().setMoving(FirstController.State.STOP);
            if (monitor != null) monitor.ISSActionCallback(this);
            Destroy(this);
        }
    }
}
