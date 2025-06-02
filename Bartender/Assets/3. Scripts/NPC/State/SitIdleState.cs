using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SitIdleState : NpcState
{
    private float waitTimer = 0f;
    public SitIdleState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        waitTimer = 0f;

        Debug.Log("SitIdle���� �� �ִϸ��̼� ����");
        npc.animationHandler.SetAnimation("SitIdle", true); //Trigger �Ķ���� �̸�

        Debug.Log($"[SitAndWaitState] �ֹ� ��� ������ ���� ({npc.npcData.orderDelay}��)");

    }

    public override void Update()
    {
        // ���⿡ ���� �޾Ҵ��� �ȹ޾Ҵ��� Ȯ���ϴ� ���� (�Լ�) 


        // 1: �ֹ� ���̸� �� Ÿ�̸� �۵�
        if (!npc.npcData.hasOrdered)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= npc.npcData.orderDelay)
            {
                Debug.Log("[SitIdleState] �ֹ� ������ �Ϸ� �� OrderState�� ��ȯ");

                npc.animationHandler.SetAnimation("SitIdle", false);
                npc.ChangeState(new OrderState(npc));
            }

            return;
        }


        // 2: �ֹ��� �߰�, ����� ���� �� ����
        if (npc.npcData.hasOrdered && !npc.npcData.hasDrink)
        {
            Debug.Log("[SitIdleState] ���� ���� ��� �� WaitForDrinkState");
        }

        // 3: �ֹ��� �߰�, ���� �޾Ҵ�.
        if (npc.npcData.hasOrdered && npc.npcData.hasDrink)
        {
            npc.animationHandler.SetAnimation("SitIdle", false);
            npc.ChangeState(new DrinkState(npc));
        }
    }
}

