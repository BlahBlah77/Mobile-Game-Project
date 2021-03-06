﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour, IDamagerer {

    public float ttd = 1.0f;

    [SerializeField]
    private int damageDealt;
    public int DamageDealt { get { return damageDealt; } }

    void OnEnable()
    {
        Invoke("DisableAndDestroy", ttd);
    }

    void DisableAndDestroy()
    {
        GameObject particle = Pickups_Particle_Pooling.pickupPool.GetDamageParticle();
        if (particle == null) return;
        particle.transform.position = this.transform.position;
        particle.transform.rotation = this.transform.rotation;
        particle.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        //Ensures that there are no possible errors
        StopAllCoroutines();
        CancelInvoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") || (other.tag == "Terrain"))
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        //Waits for a small amount of time so collisions do not overlap 
        //And cancel out the damage collision on the player
        yield return new WaitForSeconds(0.01f);
        DisableAndDestroy();
    }
}
