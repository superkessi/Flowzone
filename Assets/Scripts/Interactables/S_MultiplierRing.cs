public class S_MultiplierRing : S_Interactable
{
    protected override void ApplyEffect(S_Player player)
    {
        AddMultiplier?.Invoke();
        S_PlayerParticles.Instance.RingParticles.Play();
    }
}