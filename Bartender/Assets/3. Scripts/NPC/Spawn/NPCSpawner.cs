using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    public void SpawnNPC()
    {
        GameObject npcObj = Instantiate(npcPrefab, transform.position, Quaternion.identity);

        NPCController npcController = npcObj.GetComponent<NPCController>();

        // 1. Spawn ��ġ ����
        npcController.npcData.spawnPoint = this.transform;

        // 2. �ʱ�ȭ
        npcController.npcData.ResetNPCData();  // ������ �� �ʱ� ���� �ʱ�ȭ
        npcController.stateMachine.ChangeState(new MoveToSeatState(npcController));

        Debug.Log("NPC Spawn �Ϸ� �� �ʱ� ���� ����");
    }
}
