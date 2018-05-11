using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Subject
{

    private bool isLive;
    private Vector3 position;
    private float speed;

    Vector3 movement;


    // Use this for initialization
    void Start()
    {
        isLive = true;
        speed = 8.0f;
    }

    public override void Attach(IHandle h)
    {
        listeners.Add(h);
    }

    public override void Detach(IHandle h)
    {
        listeners.Remove(h);
    }

    public override void Notify(bool live, Vector3 pos)
    {
        foreach (IHandle h in listeners)
        {
            h.Reaction(live, pos);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Patrol")) 
        {
            isLive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        Notify(isLive, position);
    }

    public void move(float h, float v)
    {
        if (isLive)
        {
            movement.Set(h, 0, v);
            movement = movement.normalized * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(transform.position + movement);
        }
    }

    public void turn(float h, float v)
    {
        if (isLive)
        {
            movement.Set(h, 0, v);
            Quaternion rotation = Quaternion.LookRotation(movement);
            GetComponent<Rigidbody>().rotation = rotation;
        }
    }
}