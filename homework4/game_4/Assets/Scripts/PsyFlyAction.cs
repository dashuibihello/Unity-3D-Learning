using UnityEngine;
using System.Collections;

public class PsyFlyAction : SSAction
{
    private Vector3 start_vector;
    private Vector3 vertical_vector = Vector3.zero;
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
