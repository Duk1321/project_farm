using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public virtual void Hit()
    {

    }

    public virtual bool CanbeHit(List<ResourceNodeType> canBeHit)
    {
        return true;
    }
}
