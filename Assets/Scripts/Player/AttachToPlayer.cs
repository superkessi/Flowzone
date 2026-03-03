using System;
using UnityEngine;

public class AttachToPlayer : MonoBehaviour
{
    S_Player player;
	[SerializeField] private float yOffset = 20f; 

    private void Awake()
    {
        player = FindFirstObjectByType<S_Player>();
    }

    void Update()
    {
        if (!player)
            return;
        transform.position = new Vector3(player.transform.position.x, -yOffset, player.transform.position.z);
    }
}
