using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class PlayerNameScore : MonoBehaviour
{
    //an instance to save player name and score and to transit it from scene to scene
    public static PlayerNameScore Instance;
     
    
    void Start()
    {        
       
    }
    void Update()
    {
    
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    


}
