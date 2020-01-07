using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject instructionPanel;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        instructionPanel.SetActive(false);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

    public void OnMainPlayBtn()
    {
        mainMenuPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    public void OnDonateBtn()
    {
        Application.OpenURL("https://giving.oufoundation.org/OnlineGivingWeb/Giving/OnlineGiving/SCR");
    }

    public void OnGoBtn()
    {
        SceneManager.LoadScene("Game");
    }
}
