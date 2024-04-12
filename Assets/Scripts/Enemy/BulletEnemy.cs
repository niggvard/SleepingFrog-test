using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    [Header("BulletEnemySettings")]
    [SerializeField] private Sprite sprite;

    protected override void OnSpawn()
    {
        animator.enabled = false;
        spriteRenderer.sprite = sprite;

        Destroy(gameObject, 5);
    }

    protected override IEnumerator ShowDamage() 
    {
        yield return new WaitForSeconds(0);
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForSeconds(0);

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.transform == PlayerStats.player.transform)
        {
            PlayerStats.player.HP--;
            Destroy(gameObject);
        }
    }
}
