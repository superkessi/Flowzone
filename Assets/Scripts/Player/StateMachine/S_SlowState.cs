
using UnityEngine;

public class S_SlowState : S_MovementState
{
    float originalSpeed;

    public S_SlowState(S_Player player) : base(player)
    {
        this.player = player;
    }

    public override StateType Update()
    {
        player.ApplyGravity();
        player.CurrentForwardSpeed = originalSpeed * 0.5f;
        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Quick stop"))
            player.Animator.SetTrigger("QuickStop_End");

        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("QuickStop_End"))
        {
            return StateType.IDLE;
        }

        return StateType.SLOW;
    }

    public override void FixedUpdate()
    {

    }

    public override void Enter()
    {
        S_PlayerParticles.Instance.SkillParticles.Play();

        originalSpeed = player.CurrentForwardSpeed;
        player.isSlowed = true;
        player.Animator.SetTrigger("QuickStop");
    }

    public override void Exit()
    {
        player.CurrentForwardSpeed = originalSpeed;
        player.isSlowed = false;
    }
}
