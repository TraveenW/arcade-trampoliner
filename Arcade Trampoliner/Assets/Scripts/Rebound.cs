using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField] float powerMin;
    [SerializeField] float powerMax;

    [HideInInspector] public float launchForce;
    Animator despawnAnimation;
    AudioSource reboundSound;

    LineMaker lineMaker;

    private void Start()
    {
        despawnAnimation = GetComponentInChildren<Animator>();
        reboundSound = GetComponent<AudioSource>();
        lineMaker = GameObject.Find("Line Maker").GetComponent<LineMaker>();
        
        launchForce = Mathf.Sqrt(transform.localScale.x + 1) / 2;
        if (launchForce < powerMin)
        {
            launchForce = powerMin;
        }
        if (launchForce > powerMax)
        {
            launchForce = powerMax;
        }
    }

    // Despawns Rebound paddle, and plays a sound if it was caused by the ball
    public void DespawnRebound(bool gotBall = false)
    {
        if (gotBall) { reboundSound.Play(); }
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyObject());
    }

    // Delete reference to object in LineMaker script, then destroy itself
    IEnumerator DestroyObject()
    {
        if (lineMaker.launchers.Contains(gameObject)) {
            lineMaker.launchers.Remove(gameObject);
        }

        despawnAnimation.enabled = true;
        yield return new WaitForSeconds(despawnAnimation.runtimeAnimatorController.animationClips[0].length);
        Destroy(gameObject);
    }
}
