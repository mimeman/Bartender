using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutState : NpcState 
{
    private bool isWalking;

    public OutState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        if(npc.npcData.spawnPoint == null)
        {
            Debug.LogWarning("Exit ���� ����Ʈ�� �������");
            return;
        }

        npc.animationHandler.SetAnimation("Walk", true);
        npc.movementHandler.MoveTo(npc.npcData.spawnPoint.position);
        isWalking = true;
    }
    public override void Update()
    {
        if (!isWalking) return;

        if (npc.movementHandler.HasReachedDestination())
        {
            Debug.Log("NPC �ⱸ ����");

            npc.animationHandler.SetAnimation("Walk", false);

            GameObject.Destroy(npc.gameObject);
        }
    }

}
