using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameController : MonoBehaviour
{
    private UI ui;
    private GameObject top_player;
    private GameObject bottom_player;

    public int topPlayerHealth = 3;         // 0
    public int bottomPlayerHealth = 3;      // 1  

    private float scoreMultiplier;
    private float score;

    private bool displayUI;

    private float bonus = 20f;


    private void Start()
    {
        ui = GetComponent<UI>();
        top_player = GameObject.FindGameObjectWithTag("Player0");
        bottom_player = GameObject.FindGameObjectWithTag("Player1");

        displayUI = false;
        scoreMultiplier = 2f;
        score = 0;
    }

    private void Update()
    {
        if (top_player != null && IsPlayerDead(0))
        {
            print("Top player has died");
            top_player.GetComponent<PlayerController>().KillPlayer();
            scoreMultiplier = 1f;
        }
        if (bottom_player != null && IsPlayerDead(1))
        {
            print("Bottom player has died");
            bottom_player.GetComponent<PlayerController>().KillPlayer();
            scoreMultiplier = 1f;
        }

        if (IsGameOver())
        {
            if (!displayUI)
            {
                ui.OnGameOver();
                displayUI = true;
            }            
        }

        if (!IsGameOver())
            score += scoreMultiplier * Time.deltaTime;
    }

    public void DecreaseHealth(int player)
    {
        if (player == 0)
        {
            topPlayerHealth--;
            ui.UpdatePlayerHealthIcon(0);
        }
        else
        {
            bottomPlayerHealth--;
            ui.UpdatePlayerHealthIcon(1);
        }
    }

    public void GainPoints()
    {
        score += bonus;
    }

    private bool IsPlayerDead(int player)
    {
        // Top player
        if (player == 0)
        {
            if (topPlayerHealth <= 0)
                return true;
        }
        // Bottom player
        else
        {
            if (bottomPlayerHealth <= 0)
                return true;
        }
        return false;
    }

    private bool IsGameOver()
    {
        return IsPlayerDead(0) && IsPlayerDead(1);
    }

    public float GetScore()
    {
        return score;
    }

    public void InstKillPlayer(int player)
    {
        if (player == 0)
        {
            topPlayerHealth = 0;
            ui.UpdatePlayerHealthIcon(0);
        }
        else
        {
            bottomPlayerHealth = 0;
            ui.UpdatePlayerHealthIcon(1);
        }
    }
}
