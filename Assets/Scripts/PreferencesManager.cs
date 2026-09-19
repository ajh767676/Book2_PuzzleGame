using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreferencesManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Dropdown pictureDropdown;
    public Slider timeSlider;
    public TMP_Text timeValueText;

    void Start()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName", "");
        pictureDropdown.value = PlayerPrefs.GetInt("PictureChoice", 0);
        timeSlider.value = PlayerPrefs.GetFloat("TimeLimit", 60f);

        UpdateTimeText(timeSlider.value);
    }

    public void UpdateTimeText(float value)
    {
        timeValueText.text = value.ToString("0") + " seconds";
    }

    public void StartGame()
    {
        string playerName = nameInput.text.Trim();

        if (playerName == "")
        {
            playerName = "Player";
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PictureChoice", pictureDropdown.value);
        PlayerPrefs.SetFloat("TimeLimit", timeSlider.value);
        PlayerPrefs.Save();

        SceneManager.LoadScene("chapterPuzzle");
    }
}