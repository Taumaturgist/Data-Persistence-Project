using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;
    
    //score & best score
    private bool m_Started = false;
    public int m_Points;
    public int m_BestPoints;

    //player name & best player name
    public string playerName;
    public string bestPlayerName;
    public Text currentName;
        
    public bool m_GameOver = false;

    public void Awake()
    {
       
        Instance = this;
        
        if (PlayerPrefs.HasKey("SavePoints") && PlayerPrefs.HasKey("SaveBestName"))
        {
            m_BestPoints = PlayerPrefs.GetInt("SavePoints");
            bestPlayerName = PlayerPrefs.GetString("SaveBestName");
        }        
        if (PlayerPrefs.HasKey("SaveName"))
        {
            playerName = PlayerPrefs.GetString("SaveName");
            currentName.text = "Current Player : " + playerName;
        }     
        
    }
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(Instance.AddPoint);
            }
        }   
        
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        BestScoreText.text = "Best Score : " + bestPlayerName + " : " + m_BestPoints;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        BestPointsCounter();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
    public void BestPointsCounter()
    {
        if (m_Points > m_BestPoints)
        {
            m_BestPoints = m_Points;
            bestPlayerName = playerName;
            
        }
        PlayerPrefs.SetInt("SavePoints", m_BestPoints);
        PlayerPrefs.SetString("SaveBestName", bestPlayerName);

    }
    public void BestNameSave()
    {

    }
    public void ResetPoints()
    {
        m_Points = 0;
    }
    public void ResetBestPoints()
    {
        PlayerPrefs.DeleteKey("SavePoints");
        PlayerPrefs.DeleteKey("SaveBestName");
        m_BestPoints = 0;
        bestPlayerName = "none";
    }
    
}
