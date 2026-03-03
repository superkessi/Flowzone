using System;
using UnityEngine;

public enum PickupType
{
    NONE,
    JUMP,
    SLOW
}

public class S_Pickup : MonoBehaviour 
{
    [SerializeField] PickupData pickupData;
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        var player = other.GetComponent<S_Player>();
        if (player)
        {
            player.CollectPickup(pickupData);
            gameObject.SetActive(false);
        }
    }
}