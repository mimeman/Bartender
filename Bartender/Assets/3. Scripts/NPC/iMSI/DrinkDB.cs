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
            Debug.LogWarning("DrinkDB: 음료 리스트가 비어있습니다.");
            return null;
        }

        return drinkList[Random.Range(0, drinkList.Count)];
    }
}
