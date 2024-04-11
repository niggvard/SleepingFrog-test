using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpAndScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText, scoreText;

    private void Update()
    {
        hpText.text = PlayerStats.player.HP.ToString();
        scoreText.text = PlayerStats.player.score.ToString();
    }
}
