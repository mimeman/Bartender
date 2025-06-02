using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkDB : MonoBehaviour
{
    public List<DrinkData> drinkList;

    public DrinkData GetRandomDrink()
    {
        if (drinkList == null || drinkList.Count == 0)
        {
            Debug.LogWarning("DrinkDB: ���� ����Ʈ�� ����ֽ��ϴ�.");
            return null;
        }

        return drinkList[Random.Range(0, drinkList.Count)];
    }
}
