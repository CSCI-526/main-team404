using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ClimbTutorialRegion : MonoBehaviour
{
    public DeflectTutorialUI part1;
    public DeflectTutorialUI part2;
    public DeflectTutorialUI part3;
    public DeflectTutorialUI part4;
    public float timeToWait = 2f;
    private bool tutorialDone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !tutorialDone)
        {
            tutorialDone = true;
            StartCoroutine(StartTutorial());
        }
    }

    private IEnumerator StartTutorial()
    {
        // Disable player inputs
        PlayerInput.instance.DisableGamePlayInputs();

        // Show all UI buttons
        part1.Show();

        // Wait for timeToWait seconds, then show part1
        yield return new WaitForSeconds(timeToWait);
        part2.Show();

        // Wait for timeToWait seconds, then show part2
        yield return new WaitForSeconds(timeToWait);
        part3.Show();

        // Wait for timeToWait seconds, then show part3
        yield return new WaitForSeconds(timeToWait);
        part4.Show();

        // Wait for timeToWait seconds, then enable player inputs
        yield return new WaitForSeconds(timeToWait);
        PlayerInput.instance.EnableGamePlayInputs();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && tutorialDone)
        {
            // Hide all UI buttons and parts
            
            part1.Hide();
            part2.Hide();
            part3.Hide();
            part4.Hide();
        }
    }
}
