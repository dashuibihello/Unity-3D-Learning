using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    
    private List<SSAction> waitingAdd = new List<SSAction>();                       
    private List<int> waitingDelete = new List<int>();                                           

    protected void Update()
    {
        foreach (SSAction myaction in waitingAdd)
        {
            actions[myaction.GetInstanceID()] = myaction;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction myaction = kv.Value;
            if (myaction.destroy)
            {
                waitingDelete.Add(myaction.GetInstanceID());
            }
            else if (myaction.enable)
            {
                myaction.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction myaction = actions[key];
            actions.Remove(key);
            DestroyObject(myaction);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
    }
}