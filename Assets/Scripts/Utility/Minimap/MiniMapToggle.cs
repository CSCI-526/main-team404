using UnityEngine;

public class MiniMapToggle : MonoBehaviour
{
    public GameObject miniMapPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMapPanel.SetActive(!miniMapPanel.activeSelf);
        }
    }
}
