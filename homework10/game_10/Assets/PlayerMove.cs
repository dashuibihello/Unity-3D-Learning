using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    Vector3 movement;
    public GameObject bulletPrefab;
    int num = 0;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical") ;
        turn(x, z);
        move(x, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Command function is called from the client, but invoked on the server
            CmdFire();
        }
    }

    void move(float h, float v)
    {
        if(h !=0 || v != 0)
            transform.position += transform.forward * 0.1f;
    }

    void turn(float h, float v)
    {
        if (h != 0 || v != 0)
            movement.Set(h, 0, v);
        transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        Vector3 temp = new Vector3(0, 0.2f, 0);
        num++;
        if (num <= 5)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + 2.3f * transform.forward + temp,
            Quaternion.identity);

            bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;

            // spawn the bullet on the clients
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 1.2f);
            num--;
        }

    }
}