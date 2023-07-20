using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField] float baseLaunchForce = 50;

    Animator despawnAnimation;
    AudioSource reboundSound;

    private void Start()
    {
        despawnAnimation = GetComponentInChildren<Animator>();
        reboundSound = GetComponent<AudioSource>();
    }

    // Despawns Rebound paddle, and plays a sound if it was caused by the ball
    public void DespawnRebound(bool gotBall = false)
    {
        if (gotBall) { reboundSound.Play(); }
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        despawnAnimation.enabled = true;
        yield return new WaitForSeconds(despawnAnimation.runtimeAnimatorController.animationClips[0].length);
        Destroy(gameObject);
    }
}
