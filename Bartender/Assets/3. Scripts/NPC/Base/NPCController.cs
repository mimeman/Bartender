using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Core Systems")]
    public NPCStateMachine stateMachine;                //���� ��ȯ ����

    [Header("Sub Handlers")]
    public NPCMovementHandler movementHandler;          //�̵� ����(�ټ���, ����, ����, ���� ã��)
    public NPCInteractionHandler interactionHandler;    //�ֹ� ���� ó��(Ĭ���� ����)
    public NPCAnimationHandler animationHandler;        // ���º� �ִϸ��̼� ����
    public NPCUIHandler uiHandler;                      //(NPC ���� UI ó�� (�ֹ�, ǥ��))

    [Header("Data")]
    public NPCSeatManager seatManager;
    public NPCData npcData;

    [Header("DrinkDB")]
    public DrinkDB drinkDB;

    private void Awake()
    {
        stateMachine = new NPCStateMachine();
        stateMachine.Intialize(this);
    }


    private void Start()
    {
        stateMachine.ChangeState(new MoveToSeatState(this));
    }

    private void Update()
    {
        // ���� ������Ʈ ó��
        stateMachine?.CurrentState?.Update();
    }

    public void ChangeState(NpcState newState)
    {
        stateMachine.ChangeState(newState);
    }

}
