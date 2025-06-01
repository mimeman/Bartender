using UnityEngine;

public class NPCUIHandler : MonoBehaviour
{
    public GameObject orderIcon;

    public void ShowOrderIcon()
    {
        if (orderIcon != null)
            orderIcon.SetActive(true);
    }

    public void HideOrderIcon()
    {
        if (orderIcon != null)
            orderIcon.SetActive(false);
    }
}
