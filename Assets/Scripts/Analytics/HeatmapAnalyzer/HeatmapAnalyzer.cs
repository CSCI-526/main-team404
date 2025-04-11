using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class HeatmapAnalyzer : MonoBehaviour
{
    [Header("Input")]
    public Player player;
    public Transform checkPointsParent;
    public Tilemap map;
    public int levelIndex = 0;
    public int ceilSizeDefault = 4;
    //public double cellSizeSetting = 0;


    [Header("Google Form Uploading Setting")]
    private string URL = "https://docs.google.com/forms/d/e/1FAIpQLSfa4CcFog8e_IqVMQMcTlTi2RcSpMuxXIW0ZqAprpGSX47WpQ/formResponse";

    //public static HeatmapAnalyzer instance;

    [Header("Level Info")]
    [SerializeField] private long _sessionID;
    [SerializeField] private int level;
    [SerializeField] private int checkPointIndex;
    [SerializeField] private bool completed;
    [SerializeField] private float levelWidth;
    [SerializeField] private float levelHeight;
    [SerializeField] private float levelTopLeftPointX;
    [SerializeField] private float levelTopLeftPointY;
    [SerializeField] private float levelBottomRightX;
    [SerializeField] private float levelBottomRightY;

    [Header("Heatmap Ceil Info")]
    [SerializeField] private float ceilSize;
    [SerializeField] private int ceilRowIndex;
    [SerializeField] private int ceilColumnIndex;
    [SerializeField] private double timeSpentOnTheCeil;

    [Header("Temporary Paramters")]
    [SerializeField] private Dictionary<(int, int), double> timeMap;
    private CapsuleCollider2D playerCollider;
    //private double[] checkPointsX;
    [SerializeField] private float MAPH;
    [SerializeField] private float MAPW;
    [SerializeField] private Vector2 MAP_TOP_LEFT_POINT;
    [SerializeField] private Vector2 MAP_BOTTOM_RIGHT_POINT;
    [SerializeField] public Dictionary<CheckPoint, bool> visited;
    [SerializeField] private int checkPointsN;

    public static HeatmapAnalyzer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        Debug.Log("Initialization.");
        BoundsInt bounds = map.cellBounds;
        MAPH = bounds.size.y;
        MAPW = bounds.size.x;
        MAP_TOP_LEFT_POINT = new Vector2(bounds.xMin, bounds.yMax);
        MAP_BOTTOM_RIGHT_POINT = new Vector2(bounds.xMax, bounds.yMin);

        levelHeight = MAPH;
        levelWidth = MAPW;
        levelTopLeftPointX = MAP_TOP_LEFT_POINT.x;
        levelTopLeftPointY = MAP_TOP_LEFT_POINT.y;
        levelBottomRightX = MAP_BOTTOM_RIGHT_POINT.x;
        levelBottomRightY = MAP_BOTTOM_RIGHT_POINT.y;

        completed = false;
        checkPointIndex = 0;
        level = levelIndex;
        ceilSize = ceilSizeDefault;

        playerCollider = player.GetComponent<CapsuleCollider2D>();
        visited = new Dictionary<CheckPoint, bool>();
        checkPointsN = checkPointsParent.childCount;

        initTimeMap();
    }

    void Update()
    {
        CountTime();
    }

    void OnApplicationQuit()
    { 
        Debug.Log("Application exiting...");

        if (checkPointIndex != checkPointsN)
        {
            Debug.Log("App quit before reaching all checkpoints. Uploading heatmap...");
            SendAHeatmap();
        }
        else
        {
            Debug.Log("All checkpoints completed. No upload necessary.");
        }

    }

    public void isCheckPointCompleted(CheckPoint cp)
    {

        if (checkPointIndex == 0)
        {
            Debug.Log("Checkpoint reached: " + cp.name + " (This is the first checkpoint)");
            visited[cp] = true;
            checkPointIndex++;
        }
      
        if (!visited.ContainsKey(cp))
        {
            visited[cp] = true;
            Debug.Log("Checkpoint reached: " + cp.name + " (New)");


            Debug.Log("Reached a new checkpoint: " + cp.name);
            completed = true;
            Debug.Log("Checkpoint " + checkPointIndex + " reached. Proceed to the next one.");

            SendAHeatmap();

            checkPointIndex++;
            Debug.Log("Good! Try to reach the next checkpoint.");
            completed = false;
            initTimeMap();

            if (checkPointIndex == checkPointsN)
            {
                Debug.Log("All checkpoints completed! Great job!");
            }
        }
        else
        {
            Debug.Log("Checkpoint revisited: " + cp.name);
        }
    }

    public void CountTime()
    {
        (int, int) rowCol = getPlayerCeilRowColumn();
        float deltaTime = Time.deltaTime;

        if (timeMap.TryGetValue(rowCol, out double currentTime))
        {
            timeMap[rowCol] = currentTime + deltaTime;
        }
        else
        {
            timeMap[rowCol] = deltaTime;
        }
    }

    public (int, int) getPlayerCeilRowColumn()
    {
        Vector2 playerPos = playerCollider.bounds.center;

        float horizontalDis = playerPos.x - MAP_TOP_LEFT_POINT.x;
        float verticalDis = MAP_TOP_LEFT_POINT.y - playerPos.y;

        (int, int) rowCol = getCeilRowColumn(horizontalDis, verticalDis);
        int rowNumber = rowCol.Item1;
        int columnNumber = rowCol.Item2;

        return (rowNumber, columnNumber);
    }

    public (int, int) getCeilRowColumn(float width, float height)
    {
        int rowNumber = Mathf.FloorToInt(height / ceilSize);
        int columnNumber = Mathf.FloorToInt(width / ceilSize);

        return (rowNumber, columnNumber);
    }

    public void initTimeMap()
    {
        Debug.Log("Init Time Map.");
        timeMap = new Dictionary<(int, int), double>();

        
        (int, int) rowCol  = getCeilRowColumn(levelWidth, levelHeight);
        int rowN = rowCol.Item1;
        int columnN = rowCol.Item2;

        for (int i = 0; i <= rowN; i++)
        {
            for (int j = 0; j <= columnN; j++)
            {
                var coord = (i, j);
                timeMap.Add(coord, 0.0);
                
            }
        }
    }

    public void SendAHeatmap()
    {
        Debug.Log("Send to Google.");
        SendToGoogle.instance.Send();
        foreach(var entry in timeMap){
            if (entry.Value > 1e-5)
            {
                SendACeil(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }
        }
    }

    public void SendACeil(int ceilRowIndex, int ceilColumnIndex, double timeSpentOnTheCeil)
    {
        StartCoroutine(Post(ceilRowIndex, ceilColumnIndex, timeSpentOnTheCeil));
    }

    IEnumerator Post(int ceilRowIndex, int ceilColumnIndex, double timeSpentOnTheCeil)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.510782445", _sessionID.ToString());   // Session ID
        form.AddField("entry.1043537761", level.ToString());       // Level
        form.AddField("entry.1095019868", checkPointIndex.ToString());  // CheckPoint
        form.AddField("entry.1424100887", completed.ToString().ToLower());  // Completed (boolean to 1/0)
        form.AddField("entry.1132444678", levelTopLeftPointX.ToString());  // Level top-left point x
        form.AddField("entry.137265583", levelTopLeftPointY.ToString());  // Level top-left point y
        form.AddField("entry.279170956", levelBottomRightX.ToString()); // Level bottom-right point x
        form.AddField("entry.1738846031", levelBottomRightY.ToString()); // Level bottom-right point y
        form.AddField("entry.961393697", levelWidth.ToString());   // Level width
        form.AddField("entry.1625854803", levelHeight.ToString()); // Level height
        form.AddField("entry.369261424", ceilSize.ToString());     // Ceil size
        form.AddField("entry.426122945", ceilRowIndex.ToString()); // Ceil row index
        form.AddField("entry.1682497881", ceilColumnIndex.ToString()); // Ceil column index
        form.AddField("entry.498027340", timeSpentOnTheCeil.ToString("F5")); // Time spent on the ceil

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Upload failed: " + www.error);
        }
        else
        {
            Debug.Log("Data successfully uploaded!");
        }
    }
}
