public class S_MultiplierInteractable : S_Interactable
{
    protected override void ApplyEffect(S_Player player)
    {
        AddMultiplierPoint?.Invoke();
        S_PlayerParticles.Instance.CollectableParticles.Play();
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.Collectables);
    }
}