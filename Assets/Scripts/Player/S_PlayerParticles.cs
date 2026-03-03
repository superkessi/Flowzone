using System;
using UnityEngine;

public class S_PlayerParticles : MonoBehaviour
{
    public static S_PlayerParticles Instance;
    
    [SerializeField] private ParticleSystem collectableParticles;
    public ParticleSystem CollectableParticles => collectableParticles;
    [SerializeField] private ParticleSystem skillParticles;
    public ParticleSystem SkillParticles => skillParticles;
    [SerializeField] private ParticleSystem ringParticles;
    public ParticleSystem RingParticles => ringParticles;

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
