using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats player;

    public int HP
    {
        get { return playerHP; }
        set
        {
            playerHP = value;
            if (playerHP <= 0 && isAlive)
                PlayerKill();
        }
    }
    public int score { get; private set; }
    public bool canGetDamage { get; private set; }

    [SerializeField] private int playerHP;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject afterDeathMenu;
    [SerializeField] private Transform attackZoneCollider;
    private bool isAlive;

    private void Start()
    {
        player = this;
        isAlive = true;
    }

    public void AddScore(int value)
    {
        score += value;
    }

    private void PlayerKill()
    {
        isAlive = false;
        StartCoroutine(DeathCooldown());
    }

    private IEnumerator DeathCooldown()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        deathSound.Play();
        yield return new WaitForSeconds(1.5f);
        afterDeathMenu.SetActive(true);
        Destroy(player.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == attackZoneCollider)
            canGetDamage = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == attackZoneCollider)
            canGetDamage = false;
    }
}