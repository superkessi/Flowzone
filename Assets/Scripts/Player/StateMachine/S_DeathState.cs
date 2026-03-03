using System;
using UnityEditor;
using UnityEngine;

public class S_DeathState : S_MovementState
{
    public S_DeathState(S_Player player) : base(player)
    {

    }

    public override StateType Update()
    {
        player.ApplyGravity();
        
        return StateType.DEAD;
    }

    public override void FixedUpdate()
    {

    }

    public override void Enter()
    {
        var paricles = GameObject.Instantiate(player.DeathParticles.gameObject, 
            player.transform.position + new Vector3(0,1.5f,-3),
            player.transform.rotation);
        S_Player.OnPlayerDeath?.Invoke();
        player.DestroyPlayer();
    }

    public override void Exit()
    {
        player.transform.Rotate(new Vector3(-75f, 0, 0));
        player.Controller.excludeLayers = 0;
    }
}