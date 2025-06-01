using UnityEngine;

public class NPCData : MonoBehaviour
{
    [Header("NPC ����")]
    public bool isMove = false;         // ���� ������
    public bool isArrived = false;      // ���� ���� ����
    public bool isSeated = false;       // ���� �Ϸ� ���θ� ����
    public bool hasOrdered = false;     // �ֹ� �Ϸ� ����
    public bool hasDrink = false;       // ���� �޾Ҵ��� ����
    public bool isFinished = false;     // ��� �ൿ �Ϸ� ����


    [Header("���� ��")]
    [Range(0f, 10f)]
    public float drinkScore = 0f;       // ���� ����

    [Header("Ÿ�̸�")]
    [Tooltip("�ֹ��ϱ� �� ��� �ð�")]
    public float orderDelay = 0f;       // �ֹ� �� ���� ������

    [Tooltip("���Ḧ ���ô� �� �ð�")]
    public float drinkDuration = 180f;  // ���ô� �ð� (3��)

    // NPC �ʱ�ȭ (���� ��)
    public void ResetNPCData()
    {
        isSeated = false;
        drinkScore = 0f;
        orderDelay = 0f;
        hasOrdered = false;
        hasDrink = false;
        isFinished = false;
        isArrived = false;

        // ���� �ֹ� ������ ���� (5~7��)
        orderDelay = Random.Range(5f, 7f);

        // ���� ���� �ð� ���� (2~4��)
        drinkDuration = Random.Range(120f, 240f);
    }

    private void Start()
    {
        // ������ �� ���� ���� �ʱ�ȭ
        if (orderDelay == 0f)
            orderDelay = Random.Range(1f, 5f);

        if (drinkDuration == 180f) // �⺻���̸�
            drinkDuration = Random.Range(120f, 240f);
    }
}