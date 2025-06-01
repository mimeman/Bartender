using UnityEngine;
using UnityEngine.AI;

public class NPCMovementHandler : MonoBehaviour
{
    // NPC가 사용할 NavMeshAgent 컴포넌트
    private NavMeshAgent agent;

    // NPCSeatManager 참조 (의자 관리는 이것만 사용)
    private NPCSeatManager seatManager;

    [Header("Exit")]
    [Tooltip("퇴장 시 이동할 문(출구)의 Transform")]
    public Transform exitPosition;

    private void Awake()
    {
        // NavMeshAgent 컴포넌트 참조
        agent = GetComponent<NavMeshAgent>();

        // NPCSeatManager 참조 (씬에서 찾거나 같은 GameObject에서 찾기)
        seatManager = FindObjectOfType<NPCSeatManager>();
        if (seatManager == null)
        {
            Debug.LogError("NPCSeatManager를 찾을 수 없습니다!");
        }
    }

    // 목적지로 이동 명령
    /*    public void MoveTo(Vector3 destination)
        {
            if (agent == null) return;
            agent.SetDestination(destination);
        }*/

    public void MoveTo(Vector3 destination)
    {
        if (agent == null) return;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // 안전한 NavMesh 위치로 이동
        }
        else
        {
            Debug.LogWarning("NavMesh 위의 유효한 지점을 찾지 못했습니다.");
        }
    }

    // 목적지 도착 여부 확인
    public bool HasReachedDestination()
    {
        if (agent == null) return false;
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance &&
               (!agent.hasPath || agent.velocity.sqrMagnitude < 0.2f);
        
    }

    // 빈 의자 할당 (NPCSeatManager를 통해서만)
    public Transform AssignSeat()
    {
        if (seatManager == null) return null;
        return seatManager.AssignSeat();
    }

    // 의자 반납 (NPCSeatManager를 통해서만)
    public void ReleaseSeat(Transform seat)
    {
        if (seatManager != null && seat != null)
        {
            seatManager.ReleaseSeat(seat);
        }
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