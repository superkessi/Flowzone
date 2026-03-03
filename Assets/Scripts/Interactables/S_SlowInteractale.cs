using UnityEngine;

public class S_SlowInteractale : S_Interactable
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float duration;
    [SerializeField] private float cooldown;
    [SerializeField] private float scorePoints;
    
    protected override void ApplyEffect(S_Player player) 
    {
       
    }
}