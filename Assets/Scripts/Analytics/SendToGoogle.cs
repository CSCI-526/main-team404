using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SendToGoogle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeY4A1l1x3K3anho5MYdgZsPemq6980CobLuXJ0fFmSzpvOiQ/formResponse";
    public static SendToGoogle instance;
    private long _sessionID;
    public int parryAttempted;
    public int parrySuccessful;
    public int dodgeAttempted;
    public int dodgeSuccessful;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _sessionID = DateTime.Now.Ticks;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Send()
    {
        // Assign variables
        //parryAttempted = UnityEngine.Random.Range(0, 100);
        //parrySuccessful = UnityEngine.Random.Range(0, parryAttempted);

        StartCoroutine(Post(_sessionID.ToString(), 
            parryAttempted.ToString(), parrySuccessful.ToString(), 
            dodgeAttempted.ToString(), dodgeSuccessful.ToString()));
    }

    IEnumerator Post(string sessionID, 
        string parryAttempted, string parrySuccessful, 
        string dodgeAttempted, string dodgeSuccessful)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.139774989", sessionID);
        form.AddField("entry.2104169773", SceneManager.GetActiveScene().buildIndex.ToString());
        form.AddField("entry.353296791", parryAttempted);
        form.AddField("entry.1606299993", parrySuccessful);
        form.AddField("entry.2013387407", dodgeAttempted);
        form.AddField("entry.452177833", dodgeSuccessful);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public void AddParryCount()
    {
        parryAttempted++;
    }

    public void AddSuccessfulParryCount()
    {
        parrySuccessful++;
    }
    public void AddDodgeCount()
    {
        dodgeAttempted++;
    }

    public void AddSuccessfulDodgeCount()
    {
        dodgeSuccessful++;
    }

    public void ResetAll()
    {
        parryAttempted = 0;
        parrySuccessful = 0;
        dodgeAttempted = 0;
        dodgeSuccessful = 0;
    }
}
