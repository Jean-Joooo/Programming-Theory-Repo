using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text liveScoreText;
    public Text bestScoreText;
    public GameObject CongratsYouWon;
    public GameObject GameOverText;
  
    private string currentPlayerName; // ENCAPSULATION 
    private int bestScore; // ENCAPSULATION 
    private string bestPlayerName; // ENCAPSULATION 
    private int m_Points; // ENCAPSULATION 
    public bool m_Started = false;
    public bool IsGameOver { get { return m_GameOver; } }
    private bool m_GameOver = false;
    public GameObject PressStartText;
    private SpawnManager spawnManager; // ENCAPSULATION 

    void Awake()

       {

      Instance = this;

       }

    void Start()
    {
        LoadData();
        bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";
        liveScoreText.text = $"Score : 0";
        UpdateScoreText();
        spawnManager = FindFirstObjectByType<SpawnManager>();
        CongratsYouWon.SetActive(false);
        GameOverText.SetActive(false);

    }
    public void StartGame()
{
    m_Started = true;
    m_GameOver = false;
    
    if (PressStartText != null) 
    {
        PressStartText.SetActive(false); 
    }

    spawnManager.StartSpawning();
}
    void Update()
    {
   
        if (!m_Started && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    public void AddPoint(int point) // ABSTRACTION 
    {
        if (m_GameOver == true)
        {
            return;
        }
        m_Points += point;
        liveScoreText.text = $"Score : {m_Points}";

        if (m_Points == 10 || m_Points == 20)
        {
            spawnManager.IncreaseAlienSpeed();
        }
        if (m_Points >= 30)
        {
            Debug.Log("Congrats ! You won !");
            CongratsYouWon.SetActive(true);
            m_GameOver = true;
            spawnManager.StopSpawningAndClearScene();
        }

    }

    void UpdateScoreText()
    {
        bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}\nScore : {currentPlayerName} : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        spawnManager.StopSpawningAndClearScene();

        if (m_Points > bestScore)
        {
            bestScore = m_Points;
            bestPlayerName = currentPlayerName;
            SaveData();
        }
        
    }

    void LoadData()
    {
        currentPlayerName = PlayerPrefs.GetString("CurrentPlayerName");
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestPlayerName = PlayerPrefs.GetString("BestPlayerName", " ");
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.SetString("BestPlayerName", bestPlayerName);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}