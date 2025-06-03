using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class OrderState : NpcState
{
    public OrderState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // 1. �ֹ� �ִϸ��̼� ����
        npc.animationHandler.SetTrigger("Order");

        // 2. �ֹ� ���� ����
        npc.npcData.hasOrdered = true;

        // 3. ���� ���� �̱�
        var drink = npc.drinkDB.GetRandomDrink();
        npc.npcData.orderedDrink = drink;


        // 5. UI�� �ֹ� �̹��� ����
        npc.uiHandler.ShowOrderIcon(drink.iconSprite);


        // 6. ������ �� ���� ���·� ��ȯ
        npc.StartCoroutine(WaitForOrderToComplete());
    }

    private IEnumerator WaitForOrderToComplete() //�ڷ�ƾ�� ���⿡ ���� �����ʴµ� ������� �ӱ��
    {
        float delay = npc.npcData.orderDelay;
        Debug.Log($"[OrderState] �ֹ� ������: {delay}��");

        yield return new WaitForSeconds(delay);

        // 5. �ֹ� ������ ����
        npc.uiHandler.HideOrderIcon();

        // 6. ���� ���·� ��ȯ
        npc.ChangeState(new SitIdleState(npc));
    }

    public override void Exit()
    {
        npc.StopAllCoroutines(); // Ȥ�� �߰��� ��ȯ�Ǹ� �����ϰ� �ڷ�ƾ ����
    }
}
