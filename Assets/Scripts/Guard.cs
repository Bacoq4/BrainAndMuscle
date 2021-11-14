using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    Animator animator;
    bool b = false;
    Transform playerTransform;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            b = true;
            animator.SetTrigger("attack");
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.Health -= 20;
            player.GatheredBandageNumber -= 2;
            player.healthBar.SetHealth(player.Health);
            // if (player.GatheredBandageNumber < -5)
            // {

            //     Runner runner = other.GetComponent<Runner>();
            //     runner.Zspeed = 0;
            //     runner.animator.SetTrigger("idle");

            // }
        }
    }

    private void Update() {
        if (b)
        {
            transform.LookAt(playerTransform);
        }
    }
}
