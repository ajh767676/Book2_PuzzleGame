using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text timerText;

    private float timeRemaining;
    private bool gameEnded;
    private int placedPieces;
    private int totalPieces = 25;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        timeRemaining = PlayerPrefs.GetFloat("TimeLimit", 60f);

        playerNameText.text = "Player: " + playerName;
        UpdateTimerText();

        placedPieces = 0;
    }

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateTimerText();
            FinishGame("Time ran out!", 0);
        }
        else
        {
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    public void PiecePlaced()
    {
        if (gameEnded)
        {
            return;
        }

        placedPieces++;

        if (placedPieces >= totalPieces)
        {
            CompleteGame();
        }
    }

    public void StopGame()
    {
        FinishGame("Game stopped early.", 0);
    }

    public void CompleteGame()
    {
        int score = Mathf.CeilToInt(timeRemaining);
        FinishGame("Puzzle completed!", score);
    }

    void FinishGame(string resultMessage, int score)
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        PlayerPrefs.SetString("GameResult", resultMessage);
        PlayerPrefs.SetInt("CurrentScore", score);
        PlayerPrefs.Save();

        SceneManager.LoadScene("exit");
    }
}