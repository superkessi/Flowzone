using System.Collections;
using UnityEngine;

public class S_SpeedBoostState : S_MovementState
{
    float originalSpeed;

    public S_SpeedBoostState(S_Player player) : base(player)
    {
        this.player = player;
    }

    public override StateType Update()
    {
        player.ApplyGravity();
        player.CurrentForwardSpeed = originalSpeed * player.SpeedRingModifier;
        
        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            return StateType.IDLE;
        }

        return StateType.BOOST;
    }

    public override void FixedUpdate()
    {
    }

    public override void Enter()
    {
        originalSpeed = player.CurrentForwardSpeed;
        player.Animator.SetTrigger("Roll");
        player.SpeedParticles.Play();
    }

    public override void Exit()
    {
        player.CurrentForwardSpeed = originalSpeed;
    }
}