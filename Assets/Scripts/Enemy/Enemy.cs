using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackCooldown;

    [Header("Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Collider2D enemyCollider;
    protected Transform point1, point2, attackPoint;
    protected SpriteRenderer spriteRenderer;
    protected float direction;
    protected Vector3 defaultScale;
    protected bool isAttacking;
    protected bool isAlive;

    protected void Start()
    {
        point1 = LinkHolder.Point1;
        point2 = LinkHolder.Point2;
        attackPoint = LinkHolder.EnemyAttackPoint;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale;
        isAttacking = false;
        isAlive = true;

        transform.position = Random.Range(0, 2) == 1 ? point1.position : point2.position;
        OnSpawn();
    }

    protected virtual void OnSpawn() { }

    public virtual void GetDamage(int damage)
    {
        if (!isAlive) return;

        hp -= damage;
        
        bool wasAttaking = isAttacking;
        StopCoroutine(Attack());
        if (wasAttaking)
            StartCoroutine(Attack());

        StartCoroutine(ShowDamage());

        if (hp <= 0)
            Kill();
    }

    public virtual void Kill()
    {
        isAlive = false;
        body.isKinematic = true;
        enemyCollider.enabled = false;
        StopAllCoroutines();

        PlayerStats.player.AddScore(1);
        animator.Play("HurtAnimation");
        Destroy(gameObject, 1);
    }

    protected virtual IEnumerator ShowDamage()
    {
        var defaultColor = spriteRenderer.color;

        spriteRenderer.color = new(255, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = defaultColor;
    }

    protected virtual void FixedUpdate()
    {
        body.velocity = new Vector2(direction, 0) * speed * Time.fixedDeltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == point1)
        {
            var newScale = defaultScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            direction = 1;
        }
        else if (other.transform == point2)
        {
            transform.localScale = defaultScale;

            direction = -1;
        }
        else if (other.transform == attackPoint)
        {
            StartCoroutine(Attack());
        }
    }

    protected virtual IEnumerator Attack()
    {
        isAttacking = true;
        direction = 0;
        animator.Play("IdleAnimation");
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown / 2);
            animator.Play("PunchAnimation");
            PlayerStats.player.HP--;
            yield return new WaitForSeconds(attackCooldown / 2);
            animator.Play("IdleAnimation");
        }
    }

    protected virtual void OnDestroy()
    {
        StopAllCoroutines();
    }
}
