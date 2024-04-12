using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterDeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject newHighscoreText;
    [SerializeField] private TextMeshProUGUI totalPointsText, highscoreText;

    private void OnEnable()
    {
        Time.timeScale = 0;
        int score = PlayerStats.player.score;
        int highscore = PlayerPrefs.GetInt("highscore", 0);

        totalPointsText.text = "Total kills: " + score;
        highscoreText.text = "Highscore: " + highscore.ToString();

        if (highscore < score)
        {
            newHighscoreText.SetActive(true);
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
