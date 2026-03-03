public class S_SpeedRing : S_Interactable
{
    protected override void ApplyEffect(S_Player player)
    {
        player.ApplySpeedBoost();
        S_Player.AddMultiplier?.Invoke();
    }
}