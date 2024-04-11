using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator playerAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == ground)
        {
            playerAnimator.Play("IdleAnimation");
            playerController.canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == ground)
        {
            playerController.canJump = false;
        }
    }
}
