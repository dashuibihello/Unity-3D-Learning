using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    public float gravity = -5;                                
    private Vector3 start_vector;                              
    private Vector3 vertical_vector = Vector3.zero;             
    private float time;                                        
    private Vector3 current_angle = Vector3.zero;               

    private CCFlyAction() { }
    public static CCFlyAction GetSSAction(Vector3 direction, float power)
    {
        CCFlyAction action = CreateInstance<CCFlyAction>();
        if (direction.x == -1)
        {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, -20)) * Vector3.left * power;
        }
        else
        {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, 20)) * Vector3.right * power;
        }
        return action;
    }

    public override void Update()
    { 
        time += Time.fixedDeltaTime;
        vertical_vector.y = gravity * time;
        transform.position += (start_vector + vertical_vector) * Time.fixedDeltaTime;
        current_angle.z = Mathf.Atan((start_vector.y + vertical_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;
        if (transform.position.y < -10)
        {
            destroy = true;
            callback.SSActionEvent(this);
        }
    }

    public override void FixedUpdate() { }

    public override void Start()
    {
        gameobject.GetComponent<Rigidbody>().useGravity = false;
    }
}