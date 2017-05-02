using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionParticles : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Explode();
    }
    
    void Explode()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
