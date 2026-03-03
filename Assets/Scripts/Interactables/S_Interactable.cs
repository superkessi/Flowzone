using System;
using UnityEngine;

public abstract class S_Interactable : MonoBehaviour
{
    public static Action AddMultiplier;
    public static Action AddMultiplierPoint;
    public static Action<float, float> ApplySpeedBoost;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        var player = other.GetComponent<S_Player>();
        if (player)
        {
            ApplyEffect(player);
            gameObject.SetActive(false);
        }
    }

    protected abstract void ApplyEffect(S_Player player);
}