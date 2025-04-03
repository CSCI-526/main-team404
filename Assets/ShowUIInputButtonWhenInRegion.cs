using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowUIInputButtonWhenInRegion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<UIInputButtonFadeInOut> uiButtons;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var button in uiButtons)
            {
                button.Show();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var button in uiButtons)
            {
                button.Hide();
            }
        }
    }

}
