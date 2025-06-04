using UnityEngine;
using UnityEngine.AI;

public class NPCMovementHandler : MonoBehaviour
{
    [Header("Exit")]
    [Tooltip("퇴장 시 이동할 문(출구)의 Transform")]
    public Transform exitPosition;
    private Transform currentTarget;        //현재 이동 중인 목적지

    private NavMeshAgent agent;            // NPC가 사용할 NavMeshAgent 컴포넌트
    private NPCSeatManager seatManager;    // NPCSeatManager 참조 (의자 관리는 이것만 사용)

    private bool isMovingToAnchor = true;   // 착석 전 이동인지 여부

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        seatManager = FindObjectOfType<NPCSeatManager>();

        if (seatManager == null)
        {
            Debug.LogError("NPCSeatManager를 찾을 수 없습니다!");
        }
    }

     
    // NavMesh 목적지로 이동 (내부용)
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

    //의자 이동 시작 (Anchor로 이동)
    public void MoveToSeat(Collider  anchor, Transform sitPoint) 
    {
        if (anchorCollider == null || sitPoint == null) return;


        Debug.Log("MoveToSeat 이동");
        currentTarget = anchor;
        isMovingToAnchor = true;
        MoveTo(currentTarget.position);
    }

    // Anchor 도착 후 SitPoint로 위치 보정 (텔레포트)
    public void UpdateMovementToSitPoint(Transform sitPoint)
    {
        Debug.Log("텔포 의자로");

        currentTarget = sitPoint;
        isMovingToAnchor = false;

        if (agent != null)
        {
            agent.Warp(sitPoint.position); // 위치 강제 이동 (NavMesh 고려)
            agent.ResetPath();
        }
        else
        {
            transform.position = sitPoint.position; // Fallback
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

    // 의자 할당 (anchor, sitPoint 같이 반환)
    public (Collider anchor, Transform sitPoint) AssignSeat()
    {
        if (seatManager == null) return (null, null);

        return seatManager.AssignSeatPair();
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