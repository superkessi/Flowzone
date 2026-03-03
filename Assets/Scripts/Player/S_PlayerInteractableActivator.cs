using System;
using UnityEngine;

public class S_PlayerInteractableActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<S_Interactable>())
        {
            var allParticles = other.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach (var particle in allParticles)
                particle.Play();
        }
    }
}
