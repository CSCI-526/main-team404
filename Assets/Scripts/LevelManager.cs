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
    private CanvasGroup quoteCanvasGroup; 
    public float fadeDuration = 1.5f;
    public float displayTime = 3.0f;
    public float textFadeDuration = 1.0f; 

    private string[] quotes = {
        "\"Only those who will risk going too far can possibly find out how far one can go.\" -T.S. Eliot",
        "Thanks for playing! Live wild, seek wonder, and let your heart chase the wind",
        "\"Courage is found in unlikely places.\"  -J.R.R. Tolkien",
        "\"You cannot swim for new horizons until you have courage to lose sight of the shore.\" -William Faulkner",
        "\"A journey of a thousand miles begins with a single step.\" -Lao Tzu",
        "\"He who jumps into the void owes no explanation to those who stand and watch.\"  -Jean-Luc Godard",
        "\"To explore is to live; to settle is to fade.\"  -Dev Team",
        "\"Not I, nor anyone else can travel that road for you. You must travel it yourself.\"  -W.W",
        "\"A river carves stone not by force, but by never stopping.\" -Dev Team",
    };

    public static LevelManager instance;
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

    void Start()
    {
        if (quoteText != null)
        {
            quoteText.gameObject.SetActive(false); 
        }
    }
    void SetupCanvas()
    {
        if (GameObject.FindGameObjectWithTag("EditorOnly") == null)
        {
            GameObject canvasInstance = Instantiate(canvasPrefab);
            DontDestroyOnLoad(canvasInstance);
        }

        FindUIElements();
    }

    void FindUIElements()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("EditorOnly");
        if (canvas != null)
        {
            fadePanel = canvas.transform.Find("FadePanel")?.GetComponent<Image>();
            quoteText = canvas.transform.Find("QuoteText")?.GetComponent<TextMeshProUGUI>();
            quoteCanvasGroup = quoteText.GetComponent<CanvasGroup>(); 
            if (quoteCanvasGroup == null)
            {
                Debug.LogWarning("CanvasGroup not found. Adding one to the quote text object.");
                quoteCanvasGroup = quoteText.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartTransitionToNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartTransitionToRestartLevel();
        }
    }

    // Public function for changing to the next level
    public void StartTransitionToNextLevel()
    {
        StartCoroutine(TransitionToNextLevel());
    }

    public void StartTransitionToRestartLevel()
    {
        StartCoroutine(TransitionToRestartLevel());
    }


    IEnumerator TransitionToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            yield return StartCoroutine(HandleLastLevel());
        }
        else
        {
            yield return StartCoroutine(HandleTransition(isNextLevel: true));
            PlayerInput.instance.DisableGamePlayInputs();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator HandleLastLevel()
    {
        if (fadePanel == null || quoteText == null || quoteCanvasGroup == null)
        {
            FindUIElements();
        }
        fadePanel.gameObject.SetActive(true);
        quoteText.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        quoteText.gameObject.SetActive(true);
        quoteText.text = quotes[1]; 
        quoteCanvasGroup.alpha = 1f; 
    }

    IEnumerator TransitionToRestartLevel()
    {
        yield return StartCoroutine(HandleTransition(isNextLevel: false));
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator HandleTransition(bool isNextLevel)
    {
        if (fadePanel == null || quoteText == null || quoteCanvasGroup == null)
        {
            FindUIElements();
        }
        fadePanel.gameObject.SetActive(true);
        quoteText.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, fadeDuration));
        quoteText.gameObject.SetActive(true);

        quoteText.text = isNextLevel ? (isFirstScene ? quotes[0] : quotes[Random.Range(2, quotes.Length-1)]) : quotes[quotes.Length - 1];
        if (isFirstScene && isNextLevel) isFirstScene = false;

        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < textFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            quoteCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / textFadeDuration);
            yield return null;
        }
        quoteText.gameObject.SetActive(false);
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