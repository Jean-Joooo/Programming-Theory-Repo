using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    public Text bestScoreText;
    private int bestScore; // ENCAPSULATION 
    private string bestPlayerName; // ENCAPSULATION 
    public TMP_InputField nameInputField;
    public void StartNew()
    {
        PlayerPrefs.SetString("CurrentPlayerName", nameInputField.text);
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        LoadData();
        bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";
    }
    void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestPlayerName = PlayerPrefs.GetString("BestPlayerName", " ");
    }
    public void Exit()
    {

        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
       
#else
            Application.Quit();
#endif
    }
}