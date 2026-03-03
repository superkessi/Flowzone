
    using UnityEngine;

    public class S_HitState : S_MovementState
    {
        public S_HitState(S_Player player) : base(player)
        {
        }

        public override StateType Update()
        {
            if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit-Links") || 
                player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit-Rechts"))
            {
                Debug.Log("Hit animation finished");
                
                return StateType.IDLE;
            }
            return StateType.HIT;
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Enter()
        {
            S_Player.ResetMultiplier?.Invoke();
            Debug.Log($"Player Hit Direction: {player.hitDirection}");

            player.Animator.SetTrigger(player.hitDirection > 0 ? "Hit_Left" : "Hit_Right");
        }

        public override void Exit()
        {
            // player.hitDirection = 0;
            // player.Animator.SetFloat("Hit", 0);
        }
    }