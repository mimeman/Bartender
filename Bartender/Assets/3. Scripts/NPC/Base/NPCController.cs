using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Core Systems")]
    public NPCStateMachine stateMachine;                //상태 전환 로직

    [Header("Sub Handlers")]
    public NPCMovementHandler movementHandler;          //이동 관련(줄서기, 퇴장, 입장, 의자 찾기)
    public NPCInteractionHandler interactionHandler;    //주문 정보 처리(칵테일 정보)
    public NPCAnimationHandler animationHandler;        // 상태별 애니메이션 관리
    public NPCUIHandler uiHandler;                      //(NPC 위에 UI 처리 (주문, 표정))

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
        // 상태 업데이트 처리
        stateMachine?.CurrentState?.Update();
    }

    public void ChangeState(NpcState newState)
    {
        stateMachine.ChangeState(newState);
    }

}
