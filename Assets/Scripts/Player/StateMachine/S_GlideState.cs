using UnityEngine;

public class S_GlideState : S_MovementState
{
    public S_GlideState(S_Player player) : base(player)
    {
    }

    public override StateType Update()
    {
        player.ApplyGravity();
        
        if (player.IsGrounded)
        {
            return StateType.IDLE;
        }

        if (player.PlayerInput.actions["Dive"].inProgress)
        {
            return StateType.DIVE;
        }

        return StateType.GLIDE;
    }

    public override void FixedUpdate()
    {
    }

    public override void Enter()
    {
        player.IsGliding = true;
        //player.Velocity = Vector3.zero;
    }

    public override void Exit()
    {
        player.IsGliding = false;
    }
}