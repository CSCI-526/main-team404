using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject canvasPrefab;
    private Image fadePanel;
    private TextMeshProUGUI quoteText;
    public float fadeDuration = 1.5f;
    public float displayTime = 3.0f;

    private string[] quotes = {
        "\"Only those who will risk going too far can possibly find out how far one can go.\" — T.S. Eliot",
        "\"Courage is found in unlikely places.\" — J.R.R. Tolkien",
        "\"You cannot swim for new horizons until you have courage to lose sight of the shore.\" — William Faulkner",
        "\"A journey of a thousand miles begins with a single step.\" — Lao Tzu",
        "\"He who jumps into the void owes no explanation to those who stand and watch.\" — Jean-Luc Godard",
        "\"To explore is to live; to settle is to fade.\" — Dev Team",
        "\"Not I, nor anyone else can travel that road for you. You must travel it yourself.\" — W.W"
    };

    private static LevelManager instance;
    private bool isFirstScene = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupCanvas();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupCanvas()
    {
        if (GameObject.Find("Canvas") == null)
        {
            GameObject canvasInstance = Instantiate(canvasPrefab);
            DontDestroyOnLoad(canvasInstance);
        }

        FindUIElements();
    }

    void FindUIElements()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            fadePanel = canvas.transform.Find("FadePanel")?.GetComponent<Image>();
            quoteText = canvas.transform.Find("QuoteText")?.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(TransitionToNextLevel());
        }
    }

    IEnumerator TransitionToNextLevel()
    {
        if (fadePanel == null || quoteText == null)
        {
            FindUIElements();
        }

        fadePanel.gameObject.SetActive(true);
        quoteText.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        quoteText.gameObject.SetActive(true);

        if (isFirstScene)
        {
            quoteText.text = quotes[0]; 
            isFirstScene = false;
        }
        else
        {
            quoteText.text = quotes[Random.Range(1, quotes.Length)]; 
        }

        yield return new WaitForSeconds(displayTime);

        quoteText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Last scene reached. No further transitions.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupCanvas();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = fadePanel.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadePanel.color = color;
            yield return null;
        }
    }
}
