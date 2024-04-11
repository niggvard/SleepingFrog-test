using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats player;

    public int HP
    {
        get { return playerHP; }
        set
        {
            playerHP = value;
            if (playerHP <= 0)
                PlayerKill();
        }
    }
    public int score { get; private set; }

    [SerializeField] private int playerHP;

    private void Start()
    {
        player = this;
    }

    public void AddScore(int value)
    {
        score += value;
    }

    private void PlayerKill()
    {
        print("killed");
    }
}