using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float inputCooldown;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [HideInInspector] public bool canJump;

    [Header("Attack Settings")]
    [SerializeField] private Transform raycasterObject;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange;

    private Animator animator;
    private Rigidbody2D body;
    private Vector3 defaultScale;
    private bool canReadInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
        canReadInput = true;
        canJump = false;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (!canReadInput || !canJump) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            var newScale = defaultScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localScale = defaultScale;

            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("JumpAnimation");
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            return;
        }

        StartCoroutine(InputCooldown());
    }

    private void Attack()
    {
        animator.Play("PunchAnimation");

        var direction = raycasterObject.right;
        if (transform.localScale.x < 0)
            direction *= -1;

        var hit = Physics2D.RaycastAll(raycasterObject.position, direction, attackRange);

        if (hit.Length < 2) return;

        for (int i = 0; i < hit.Length; i++)
        {
            Enemy enemy;
            if (enemy = hit[i].transform.GetComponent<Enemy>())
            {
                enemy.GetDamage(1);
            }
        }  
    }

    private IEnumerator InputCooldown()
    {
        canReadInput = false;
        yield return new WaitForSeconds(inputCooldown);
        canReadInput = true;

        if (canJump)
            animator.Play("IdleAnimation");
    }
}
