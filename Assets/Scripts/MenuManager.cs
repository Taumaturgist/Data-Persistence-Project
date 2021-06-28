using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public Text bestScoreTitle;
    public int bestScore;
    public string bestPlayerName;

    public InputField playerName;
    public string savedName;
    
// Start is called before the first frame update
private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("SavePoints"))
        {
            bestScore = PlayerPrefs.GetInt("SavePoints");
        }
        if (PlayerPrefs.HasKey("SaveBestName"))
        {
            bestPlayerName = PlayerPrefs.GetString("SaveBestName");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreTitle.text = "Best Score : " + bestPlayerName + " : " + bestScore;
    }
    public void SaveName()
    {
        savedName = playerName.text;
        PlayerPrefs.SetString("SaveName", savedName);
    }
}
