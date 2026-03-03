using UnityEngine;

public class S_IdleState : S_MovementState
{
    public S_IdleState(S_Player player) : base(player)
    {
    }

    public override StateType Update()
    {
        if (!player.IsGrounded)
        {
            if (player.isOnRamp)
            {
                return StateType.JUMP;
            }

            return StateType.GLIDE;
        }

        return StateType.IDLE;
    }

    public override void FixedUpdate()
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
    }
}