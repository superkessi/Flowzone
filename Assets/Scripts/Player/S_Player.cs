using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Player : MonoBehaviour
{
    #region Variables

// @formatter:off
    private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;
    
    private CinemachineCamera cam;
    
    private CharacterController controller;
    public CharacterController Controller => controller;
    
    [Header("Movement")]
    //[HideInInspector]
    public bool canMove = false;
    [SerializeField] private float baseForwardSpeed = 180f;
    [SerializeField] private float maxSpeed = 2000f;
    [SerializeField] private float speedIncreasePerRegion;
    [SerializeField] private float maxStageCount = 15f;
    [SerializeField] private float speedGrowthRate = 0.005f;
    private float forwardSpeed = 180f;
    public float CurrentForwardSpeed
    {
        set => forwardSpeed = value;
        get => forwardSpeed;
    }
    public float CurrentStage = 0;
    
    [Header("Strafe")]
    [SerializeField] private float strafeSpeedMultiplier = 0.25f;
    [SerializeField] private float strafeAcceleration = 200f;
    [SerializeField] private float strafeBreakFriction = 400f;
    [SerializeField] private float switchDirectionBoost = 1.5f;
    [SerializeField] private float maxStrafeTiltAngle;
    private float strafeSpeed;
    private float currentStrafeVelocity;
    
    [Header("Jumping / Gliding")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float glideGravity = -3f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float rampJumpModifier = 0.05f;
    [HideInInspector] public bool isOnRamp = false;
    
    public Vector3 Position => transform.position;
    
    private Vector3 velocity;
    public Vector3 Velocity => velocity;
    private Animator animator;
    public Animator Animator => animator;
    public bool IsGrounded => controller.isGrounded;
    private bool isGliding;
    public bool IsGliding
    {
        get => isGliding;
        set => isGliding = value;
    }
    
    float currentVelo;
    float smoothedHorizontal;
    
    [Header("Pickups / Rings")]
    [SerializeField] private float slowModifier = 0.5f;
    [SerializeField] private float pointsForPickupUsage = 100f;
    [SerializeField] private float speedRingModifier = 1.5f;
    public float SpeedRingModifier => speedRingModifier;
    [SerializeField] private float speedRingDuration = 5f;
    public float SpeedRingDuration => speedRingDuration;
    [HideInInspector] public bool isSlowed = false;
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem deathParticles;
    public ParticleSystem DeathParticles => deathParticles;
    
    [SerializeField] private ParticleSystem jumpParticles;
    public ParticleSystem JumpParticles => jumpParticles;
    
    [SerializeField] private ParticleSystem speedParticles;
    public ParticleSystem SpeedParticles => speedParticles;

    private float horizontalInput()
    {
        float targetHorizontal = playerInput.actions["Move"].ReadValue<float>();
        smoothedHorizontal = Mathf.SmoothDamp(smoothedHorizontal, targetHorizontal, ref currentVelo, 0.1f);
        animator.SetFloat("angular_velocity", smoothedHorizontal);
        return smoothedHorizontal;
    }
    
    public static Action OnPlayerDeath;
    public static Action<bool> JumpPickupCollected;
    public static Action<bool> SlowPickupCollected;
    public static Action<float> AddScore;
    public static Action AddMultiplier;
    public static Action ResetMultiplier;
    // @formatter:on 

    #endregion

    #region State Machine

    private Dictionary<S_MovementState.StateType, S_MovementState> movementStates;
    private S_MovementState currentState;
    private S_MovementState.StateType currentStateType;
    private S_MovementState.StateType forcedState = S_MovementState.StateType.NONE;

    public void SetForcedState(S_MovementState.StateType targetState)
    {
        forcedState = targetState;
    }


    [Header("State Machine")] [SerializeField]
    S_MovementState.StateType startState;

    private void InitializeMovementStates()
    {
        movementStates = new Dictionary<S_MovementState.StateType, S_MovementState>
        {
            { S_MovementState.StateType.IDLE, new S_IdleState(this) },
            { S_MovementState.StateType.GLIDE, new S_GlideState(this) },
            { S_MovementState.StateType.JUMP, new S_JumpState(this) },
            { S_MovementState.StateType.DIVE, new S_DiveState(this) },
            { S_MovementState.StateType.SLOW, new S_SlowState(this) },
            { S_MovementState.StateType.HIT, new S_HitState(this) },
            { S_MovementState.StateType.BOOST, new S_SpeedBoostState(this) },
            { S_MovementState.StateType.DEAD, new S_DeathState(this) }
        };

        currentState = movementStates[startState];
        currentStateType = startState;
    }

    private void UpdateMovementState()
    {
        var newState = currentState.Update();

        if (forcedState != S_MovementState.StateType.NONE)
        {
            newState = forcedState;
            forcedState = S_MovementState.StateType.NONE;
        }

        if (newState != currentStateType)
        {
            currentState.Exit();
            currentState = movementStates[newState];
            currentState.Enter();
            currentStateType = newState;
            //Debug.Log($"Player entered new State : {currentStateType}");
        }
    }

    #endregion

    public void Awake()
    {
        InitializeMovementStates();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cam = GetComponentInChildren<CinemachineCamera>();
        forwardSpeed = baseForwardSpeed;
    }

    private void Update()
    {
        if (PlayerInput.actions["Jump"].triggered)
            TryUsingJumpPickup();

        if (PlayerInput.actions["Slow"].triggered)
            TryUsingSlowPickup();

        UpdateMovementState();
        UpdateSpeed();

        if(canMove)
            Move();
        
        if (gameObject.activeInHierarchy)
            controller.Move(velocity * Time.deltaTime);
    }

    #region Movement Functions

    private void UpdateSpeed()
    {
        //@formatter:off
        var speedDiff = maxSpeed - baseForwardSpeed;
        
        if(!isSlowed)
        {
            forwardSpeed = Mathf.Min(maxSpeed, speedDiff * 
                                (1 - Mathf.Exp(speedGrowthRate * Mathf.Pow(CurrentStage, 2f))) / 
                                (1 - Mathf.Exp(speedGrowthRate * Mathf.Pow(maxStageCount, 2f)))
                               + baseForwardSpeed);

            if(useOldMovement)
                strafeSpeed = forwardSpeed * strafeSpeedMultiplierOldMovement;
            else
                strafeSpeed = forwardSpeed + forwardSpeed * strafeSpeedMultiplier;
        }
        else 
        {
            var ogSpeed = forwardSpeed;
            forwardSpeed = ogSpeed * slowModifier;
        }
        
        //@formatter:on
    }

    private void Move()
    {
        MoveForward();
        MoveSideways();
    }

    private void MoveForward()
    {
        velocity.z = forwardSpeed;
    }

    public bool useOldMovement = false;
    public float strafeSpeedMultiplierOldMovement = 0.5f;

    private void MoveSideways()
    {
        if (!useOldMovement)
        {
            var input = playerInput.actions["Move"].ReadValue<float>();
            var inputDir = Mathf.Sign(input);
            var absInput = Mathf.Abs(input);

            var braking = (1f - absInput) * strafeBreakFriction;
            currentStrafeVelocity *= Mathf.Clamp01(1f - braking * Time.deltaTime);

            if (absInput > 0.01f)
            {
                var changingDir = !Mathf.Approximately(Mathf.Sign(currentStrafeVelocity), inputDir) &&
                                  Mathf.Abs(currentStrafeVelocity) > 0.01f;
                var boost = changingDir ? switchDirectionBoost : 1f;

                currentStrafeVelocity += input * strafeAcceleration * boost * Time.deltaTime;
                currentStrafeVelocity = Mathf.Clamp(currentStrafeVelocity, -strafeSpeed, strafeSpeed);
            }

            velocity.x = currentStrafeVelocity;
            transform.rotation = Quaternion.Euler(0, 0, -horizontalInput() * maxStrafeTiltAngle);
        }
        else
        {
            var input = playerInput.actions["Move"].ReadValue<float>();
            velocity.x = input * strafeSpeed * Time.fixedDeltaTime * 100f;
            transform.rotation = Quaternion.Euler(0, 0, -horizontalInput() * maxStrafeTiltAngle);
        }
    }

    public void TryJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
    }

    public void TryRampJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * rampJumpModifier * -1f * gravity);
    }

    public void ApplyGravity()
    {
        if (!IsGrounded)
        {
            var targetGravity = isGliding || isSlowed ? glideGravity : gravity;
            velocity.y += targetGravity * Time.deltaTime;
        }
        else
        {
            if (!isOnRamp)
                velocity.y = -2f;
        }
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ramp"))
        {
            isOnRamp = true;
        }
    }

    public float hitDirection;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Vector3.Dot(transform.forward, -hit.normal) > 0.7f)
        {
            forcedState = S_MovementState.StateType.DEAD;
        }
        else if (Mathf.Abs(Vector3.Dot(transform.right, hit.normal)) > 0.7f)
        {
            hitDirection = Vector3.Dot(transform.right, hit.normal);
            forcedState = S_MovementState.StateType.HIT;
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.touchObsticles);
        }
    }

    #region Pickups / Rings

    private bool hasJumpPickup = false;
    public bool HasJumpPickup => hasJumpPickup;

    private bool hasSlowPickup = false;
    public bool HasSlowPickup => hasSlowPickup;

    public void CollectPickup(PickupData pickupData)
    {
        if (pickupData.pickupType == PickupType.JUMP && !hasJumpPickup)
        {
            hasJumpPickup = true;
            JumpPickupCollected?.Invoke(true);
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.CollectDodgeSkill);
        }

        if (pickupData.pickupType == PickupType.SLOW && !hasSlowPickup)
        {
            hasSlowPickup = true;
            SlowPickupCollected?.Invoke(true);
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.CollectSlowMotion);
        }
    }

    private void TryUsingJumpPickup()
    {
        if (hasJumpPickup && !isSlowed)
        {
            forcedState = S_MovementState.StateType.JUMP;
            hasJumpPickup = false;
            AddScore?.Invoke(pointsForPickupUsage);
            JumpPickupCollected?.Invoke(false);
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UseDodgeSkill);
        }
    }

    private void TryUsingSlowPickup()
    {
        if (hasSlowPickup)
        {
            forcedState = S_MovementState.StateType.SLOW;
            hasSlowPickup = false;
            AddScore?.Invoke(pointsForPickupUsage);
            SlowPickupCollected?.Invoke(false);
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UseSlowMotion);
        }
    }

    public void ApplySpeedBoost()
    {
        forcedState = S_MovementState.StateType.BOOST;
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UseSpeedBoost); //lizzy sound hinzugef�gt
    }

    #endregion

    public void DestroyPlayer()
    {
        gameObject.SetActive(false);
    }

    float smoothedIdle;

    private void RandomizeIdleAnimation()
    {
        var randomIdle = UnityEngine.Random.Range(0, 3);
        smoothedIdle = Mathf.SmoothDamp(randomIdle, smoothedIdle, ref smoothedIdle, 0.1f);
        animator.SetFloat("RandomIdle", smoothedIdle);
    }


    private void IdleAnimationFinished()
    {
        RandomizeIdleAnimation();
        Debug.Log("Idle Animation Finished");
    }

    
    #region Camera Functions
    private Coroutine fovCoroutine;
    public void ChangeCameraFOV(float fov)
    {
        if(fovCoroutine != null)
            StopCoroutine(fovCoroutine);
        fovCoroutine = StartCoroutine(SmoothFOVTransition(fov));
    }
    
    float FocalLengthToVerticalFOV(float focalLength, float sensorSize)
    {
        if (focalLength < 0.001f)
            return 180f;
        return Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorSize * 0.5f / focalLength);
    }
    float VerticalFOVToFocalLength(float verticalFOV, float sensorSize)
    {
        if (verticalFOV >= 180f)
            return 0.001f;
        return sensorSize * 0.5f / Mathf.Tan(verticalFOV * Mathf.Deg2Rad * 0.5f);
    }
    private IEnumerator SmoothFOVTransition(float targetFocalLength)
    {
        var sensorSize = cam.Lens.PhysicalProperties.SensorSize.y;
        var currentFocalLength = VerticalFOVToFocalLength(cam.Lens.FieldOfView, sensorSize);
        var targetFOV = FocalLengthToVerticalFOV(targetFocalLength, sensorSize);
        var currentFOV = cam.Lens.FieldOfView;
    
        Debug.Log($"Changing FOV from {currentFOV} to {targetFOV} (focal length: {currentFocalLength} -> {targetFocalLength})");
    
        var duration = 0.5f; // Adjust this to control transition speed
        var elapsed = 0f;
    
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var t = elapsed / duration;
        
            // Use smoothstep for eased transition (optional - you can use linear with just 't')
            t = t * t * (3f - 2f * t);
        
            var currentLerpedFOV = Mathf.Lerp(currentFOV, targetFOV, t);
            cam.Lens.FieldOfView = currentLerpedFOV;
        
            yield return null;
        }
    
        // Ensure we end exactly at the target
        cam.Lens.FieldOfView = targetFOV;
        fovCoroutine = null;
    }

    
    private Coroutine offsetCoroutine;
    public void ChangeCameraOffset(float offset)
    {
        if(offsetCoroutine != null)
            StopCoroutine(offsetCoroutine);
        offsetCoroutine = StartCoroutine(SmoothOffsetTransition(offset));
    }
    public void ChangeCameraOffsetHit(float offset)
    {
        if (offsetCoroutine != null)
            StopCoroutine(offsetCoroutine);
        offsetCoroutine = StartCoroutine(SmoothOffsetTransitionHit(offset));
    }

    private IEnumerator SmoothOffsetTransitionHit(float targetOffset)
    {
        var camFollow = GetComponentInChildren<CinemachineFollow>();
        float currentOffset = camFollow.FollowOffset.x;

        Debug.Log($"Transitioning camera offset from {currentOffset} to {targetOffset}");

        float duration = 0.1f; // Adjust this to control transition speed
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;


            float lerpedOffset = Mathf.Lerp(currentOffset, targetOffset, t);
            camFollow.FollowOffset.x = lerpedOffset;

            yield return null;
        }

        // Ensure we end exactly at the target
        camFollow.FollowOffset.x = targetOffset;
        offsetCoroutine = null;
    }

    private IEnumerator SmoothOffsetTransition(float targetOffset)
    {
        var camFollow = GetComponentInChildren<CinemachineFollow>();
        float currentOffset = camFollow.FollowOffset.y;
    
        Debug.Log($"Transitioning camera offset from {currentOffset} to {targetOffset}");
    
        float duration = 0.3f; // Adjust this to control transition speed
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
        
            // Use smoothstep for eased transition
            t = t * t * (3f - 2f * t);
        
            float lerpedOffset = Mathf.Lerp(currentOffset, targetOffset, t);
            camFollow.FollowOffset.y = lerpedOffset;
        
            yield return null;
        }
    
        // Ensure we end exactly at the target
        camFollow.FollowOffset.y = targetOffset;
        offsetCoroutine = null;
    }

    #endregion
}
