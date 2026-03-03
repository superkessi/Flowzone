using UnityEngine;

public class S_SpeedInteractable : S_Interactable
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float duration;
    [SerializeField] private float scoreMultiplier; //added by lizzy
    
    protected override void ApplyEffect(S_Player player) 
    {
        
    }
}