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
