using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcState : MonoBehaviour
{
    protected NPCController controller;

    public NpcState(NPCController controller)   //√ ±‚»≠
    {
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
