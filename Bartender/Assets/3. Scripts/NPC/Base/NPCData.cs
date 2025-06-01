using UnityEngine;

public class NPCData : MonoBehaviour
{
    [Header("NPC 상태")]
    public bool isMove = false;         // 의자 가는중
    public bool isArrived = false;      // 의자 도착 여부
    public bool isSeated = false;       // 착석 완료 여부만 유지
    public bool hasOrdered = false;     // 주문 완료 여부
    public bool hasDrink = false;       // 음료 받았는지 여부
    public bool isFinished = false;     // 모든 행동 완료 여부


    [Header("음료 평가")]
    [Range(0f, 10f)]
    public float drinkScore = 0f;       // 음료 점수

    [Header("타이머")]
    [Tooltip("주문하기 전 대기 시간")]
    public float orderDelay = 0f;       // 주문 전 랜덤 딜레이

    [Tooltip("음료를 마시는 총 시간")]
    public float drinkDuration = 180f;  // 마시는 시간 (3분)

    // NPC 초기화 (재사용 시)
    public void ResetNPCData()
    {
        isSeated = false;
        drinkScore = 0f;
        orderDelay = 0f;
        hasOrdered = false;
        hasDrink = false;
        isFinished = false;
        isArrived = false;

        // 랜덤 주문 딜레이 설정 (5~7초)
        orderDelay = Random.Range(5f, 7f);

        // 랜덤 음료 시간 설정 (2~4분)
        drinkDuration = Random.Range(120f, 240f);
    }

    private void Start()
    {
        // 시작할 때 랜덤 값들 초기화
        if (orderDelay == 0f)
            orderDelay = Random.Range(1f, 5f);

        if (drinkDuration == 180f) // 기본값이면
            drinkDuration = Random.Range(120f, 240f);
    }
}