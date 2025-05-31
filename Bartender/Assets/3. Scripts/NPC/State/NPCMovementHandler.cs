using UnityEngine;
using UnityEngine.AI;

public class NPCMovementHandler : MonoBehaviour
{
    // NPC가 사용할 NavMeshAgent 컴포넌트
    private NavMeshAgent agent;

    [Header("Raycast Settings")]
    [Tooltip("앞에 NPC가 있는지 확인할 Raycast 거리")]
    public float queueCheckDistance = 2f;

    [Tooltip("줄 서기 감지를 위한 NPC 레이어")]
    public LayerMask npcLayer;

    [Header("Seating")]
    [Tooltip("NPC가 앉을 수 있는 좌석 Transform 배열")]
    public Transform[] seatPositions;

    [Header("Exit")]
    [Tooltip("퇴장 시 이동할 문(출구)의 Transform")]
    public Transform exitPosition;

    private void Awake()
    {
        // NavMeshAgent 컴포넌트 참조
        agent = GetComponent<NavMeshAgent>();
    }

    // 목적지로 이동 명령
    public void MoveTo(Vector3 destination)
    {
        if (agent == null) return;
        agent.SetDestination(destination);
    }

    // 목적지 도착 여부 확인
    public bool HasReachedDestination()
    {
        if (agent == null) return false;

        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance &&
               (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    // 줄 서기: 앞에 NPC가 있는지 Raycast로 확인
    public bool IsFrontBlocked()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;   // 살짝 위에서 쏘기
        Vector3 direction = transform.forward;

        return Physics.Raycast(origin, direction, queueCheckDistance, npcLayer);
    }

    // 빈 의자 탐색 (비어있는 첫 번째 의자 반환)
    public Transform FindEmptySeat()
    {
        foreach (var seat in seatPositions)
        {
            if (seat.childCount == 0) // 자식이 없으면 비어있다고 판단
                return seat;
        }

        return null; // 빈 의자가 없음
    }

    // 문으로 퇴장 이동
    public void MoveToExit()
    {
        if (exitPosition != null)
        {
            MoveTo(exitPosition.position);
        }
    }
}
