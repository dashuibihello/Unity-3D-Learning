using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{

    protected List<IHandle> listeners = new List<IHandle>();

    public abstract void AddListener(IHandle listener);

    public abstract void Remove(IHandle listener);

    public abstract void Notify(bool live, Vector3 pos);
}