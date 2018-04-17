﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAction : SSAction, ISSActionCallback
{

    public List<SSAction> sequence;    
    public int repeat = -1;            
    public int start = 0;             

    public static SequenceAction GetSSAcition(int repeat, int start, List<SSAction> sequence)
    {
        SequenceAction action = CreateInstance<SequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        source.destroy = false;
        this.start++;
        if (start >= sequence.Count)
        {
            start = 0;
            if (repeat > 0)
                repeat--;
            if (repeat == 0)
            {
                destroy = true;
                callback.SSActionEvent(this);
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameobject = gameobject;
            action.transform = transform;
            action.callback = this;
            action.Start();
        }
    }

    void OnDestroy()
    {

    }
}