using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class OrderState : NpcState
{
    public OrderState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // 1. 주문 애니메이션 실행
        npc.animationHandler.SetTrigger("Order");

        // 2. 주문 상태 갱신
        npc.npcData.hasOrdered = true;

        // 3. 랜덤 음료 뽑기
        var drink = npc.drinkDB.GetRandomDrink();
        npc.npcData.orderedDrink = drink;


        // 5. UI에 주문 이미지 전달
        npc.uiHandler.ShowOrderIcon(drink.iconSprite);


        // 6. 딜레이 후 다음 상태로 전환
        npc.StartCoroutine(WaitForOrderToComplete());
    }

    private IEnumerator WaitForOrderToComplete() //코루틴을 여기에 쓰고 싶지않는데 어떻게하지 앙기모
    {
        float delay = npc.npcData.orderDelay;
        Debug.Log($"[OrderState] 주문 딜레이: {delay}초");

        yield return new WaitForSeconds(delay);

        // 5. 주문 아이콘 제거
        npc.uiHandler.HideOrderIcon();

        // 6. 다음 상태로 전환
        npc.ChangeState(new SitIdleState(npc));
    }

    public override void Exit()
    {
        npc.StopAllCoroutines(); // 혹시 중간에 전환되면 안전하게 코루틴 정리
    }
}
