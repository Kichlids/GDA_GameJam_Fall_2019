using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    private GameController gameController;

    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject topPlayerHealthIconPanel;
    public GameObject bottomPlayerHealthIconPanel;

    public TextMeshProUGUI score;
    public TextMeshProUGUI level;
    public GameObject[] topPlayerHealthIcons;
    public GameObject[] bottomPlayerHealthIcons;

    private bool isGameOver;
    private bool isPause;


    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        isGameOver = false;
        score.text = "Score: ";
        level.text = "Level 0";

        topPlayerHealthIconPanel.SetActive(true);
        bottomPlayerHealthIconPanel.SetActive(true);
        PausePanel.SetActive(false);

        Hide(GameOverPanel);

        foreach (GameObject gm in topPlayerHealthIcons)
            gm.SetActive(true);
        foreach (GameObject gm in bottomPlayerHealthIcons)
            gm.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Pause
            if (isPause)
            {
                // Continue
                ContinueGame();
            }
            else
            {
                // Pause
                Pause();
            }
        }
        if (!isGameOver)
        {
            score.text = "Score: " + gameController.GetScore().ToString("F2");
            level.text = gameController.GetComponent<Spawner>().level.ToString();
        }
            
    }

    public void UpdatePlayerHealthIcon(int player)
    {
        if (player == 0)
        {
            int health = gameController.topPlayerHealth;
            for (int i = 0; i < topPlayerHealthIcons.Length; i++)
            {
                if (i < health)
                {
                    topPlayerHealthIcons[i].SetActive(true);
                }
                else
                {
                    topPlayerHealthIcons[i].SetActive(false);
                }
            }
        }
        else
        {
            int health = gameController.bottomPlayerHealth;
            for (int i = 0; i < bottomPlayerHealthIcons.Length; i++)
            {
                if (i < health)
                {
                    bottomPlayerHealthIcons[i].SetActive(true);
                }
                else
                {
                    bottomPlayerHealthIcons[i].SetActive(false);
                }
            }
        }
    }

    public void OnPlayAgainBtn()
    {
        print("Play again");
        Hide(GameOverPanel);
        SceneManager.LoadScene("Game");
    }

    public void OnExitBtn()
    {
        Application.Quit();
    }

    public void OnGameOver()
    {
        isGameOver = true;
        Display(GameOverPanel);
    }

    private void Display(GameObject gm)
    {
        if (!gm.activeSelf)
        {
            gm.SetActive(true);
        }
    }

    private void Hide(GameObject gm)
    {
        if (gm.activeSelf)
        {
            gm.SetActive(false);
        }
    }

    private void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    private void ContinueGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
}
