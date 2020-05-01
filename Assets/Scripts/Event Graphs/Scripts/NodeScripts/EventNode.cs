using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EventNode : XNode.Node
{
    public virtual void StartEvent()
    {
        return;
    }

    public virtual Color GetColor() => Color.red;

    [Serializable]
    public class Empty { }
}
