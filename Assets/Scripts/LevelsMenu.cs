using UnityEngine;

public class LevelsMenu : MonoBehaviour
{
    public GameObject menu; // Assign your menu in the inspector

    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }
}
