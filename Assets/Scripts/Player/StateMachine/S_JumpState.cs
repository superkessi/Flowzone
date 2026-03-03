public class S_JumpState : S_MovementState
{
    public S_JumpState(S_Player player) : base(player)
    {
    }

    public override StateType Update()
    {
        player.ApplyGravity();

        if (player.Velocity.y < 0)
        {
            return StateType.GLIDE;
        }

        if (player.PlayerInput.actions["Dive"].inProgress)
        {
            return StateType.DIVE;
        }

        return StateType.JUMP;
    }

    public override void FixedUpdate()
    {
    }

    public override void Enter()
    {
        S_PlayerParticles.Instance.SkillParticles.Play();
            
        player.Animator.SetTrigger("Jump");
        player.JumpParticles.Play();
        
        if (player.isOnRamp)
        {
            player.TryRampJump();
            player.isOnRamp = false;
            return;       
        }
        
        player.TryJump();
    }

    public override void Exit()
    {
        
    }
}