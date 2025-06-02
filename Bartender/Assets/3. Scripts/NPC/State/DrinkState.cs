using UnityEngine;

public class DrinkState : NpcState
{
    private float drinkTimer = 0;

    public DrinkState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        Debug.Log("DrinkState ����");
        npc.animationHandler.SetTrigger("Drink");
    }

    public override void Update()
    {
        drinkTimer += Time.deltaTime;

        if(drinkTimer >= npc.npcData.drinkDuration)
            npc.ChangeState(new OutState(npc));


        if (npc.animationHandler.CheckFinishAnimation("Drink"))
        {
            Debug.Log("Drink �ִϸ��̼� ���� �� SitIdleState ��ȯ");
            npc.ChangeState(new SitIdleState(npc));
        }
    }
}
