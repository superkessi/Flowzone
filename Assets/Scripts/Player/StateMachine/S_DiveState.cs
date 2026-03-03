using UnityEngine;

public class S_DiveState : S_MovementState
{
    public S_DiveState(S_Player player) : base(player)
    {
    }

    public override StateType Update()
    {
        player.ApplyGravity();

        if (player.PlayerInput.actions["Dive"].WasReleasedThisFrame())
        {
            return StateType.IDLE;
        }
        
        if (player.IsGrounded)
        {
            return StateType.IDLE;
        }
        return StateType.DIVE;
    }

    public override void FixedUpdate()
    {

    }

    public override void Enter()
    {
        player.Animator.SetBool("Dive", true);
        player.ChangeCameraOffset(10);
        
    }

    public override void Exit()
    {
        player.Animator.SetBool("Dive", false);
        player.ChangeCameraOffset(4);
    }
}