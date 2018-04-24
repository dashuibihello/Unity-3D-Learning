using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;
    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

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
            newDisk.GetComponent<DiskData>().speed = 3f;
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
            newDisk.GetComponent<DiskData>().speed = 3.5f;
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
        newDisk.GetComponent<DiskData>().speed = 4.5f;
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
