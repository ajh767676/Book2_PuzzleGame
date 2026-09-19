using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;
    public Button saveScoreButton;

    private int currentScore;
    private int highScore;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        string gameResult = PlayerPrefs.GetString("GameResult", "Game ended.");

        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        resultText.text = playerName + ", " + gameResult;
        currentScoreText.text = "Current Score: " + currentScore;
        highScoreText.text = "Highest Score: " + highScore;

        saveScoreButton.gameObject.SetActive(currentScore > highScore);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", currentScore);
        PlayerPrefs.Save();

        highScore = currentScore;
        highScoreText.text = "Highest Score: " + highScore;
        saveScoreButton.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("preferences");
    }
}
