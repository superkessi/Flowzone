using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class S_ModuleMovement : MonoBehaviour
{
    #region Values

    private float forwardSpeed;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float startSpeed = 50f;
    [SerializeField] private float absMaxSpeed;
    [SerializeField] private float speedIncreasePerStage = 0.05f;
    //[SerializeField] private float sidewaysSpeed = 100f;
    [HideInInspector] public bool moveWorld = false;

    float currentVelo;
    float smoothedHorizontal;

    private float horizontalInput()
    {
        if (!useOldMovement)
        {
            if (!playerInput.actions["Move"].IsPressed())
                return 0f;
        }
        float targetHorizontal = playerInput.actions["Move"].ReadValue<float>();
        smoothedHorizontal = Mathf.SmoothDamp(smoothedHorizontal, targetHorizontal, ref currentVelo, 0.1f);
        return smoothedHorizontal;
    }

    #endregion

    private void FixedUpdate()
    {
        if (moveWorld)
            Move();
    }

    
    [Header("Sideway Movement")]
    [SerializeField] private float sidewaysSpeed = 140f;
    [SerializeField] private bool useOldMovement = false;
    [SerializeField] private float acceleration = 200f;
    [SerializeField] private float deceleration = 400f;
    float sidewaysVelocity = 0f;
    Vector3 movement;
    
    void Move()
    {
        var horizontalInput = this.horizontalInput();
        if (!useOldMovement)
        {
            if (horizontalInput != 0)
            {
                sidewaysVelocity = Mathf.MoveTowards(sidewaysVelocity, horizontalInput * sidewaysSpeed,
                    acceleration * Time.fixedDeltaTime);
                Debug.Log(sidewaysVelocity);
            }
            else
            {
                sidewaysVelocity = Mathf.MoveTowards(sidewaysVelocity, 0f, 
                     deceleration * Time.fixedDeltaTime);
            }
            movement = new Vector3(-sidewaysVelocity, 0, -forwardSpeed) * Time.deltaTime;
        }
        else
        { 
            movement = new Vector3(-horizontalInput * sidewaysSpeed, 0, -forwardSpeed) * Time.deltaTime;
        }
        transform.Translate(movement);
    }

    public void StopMovement()
    {
        moveWorld = false;
    }

    public void UpdateSpeed(int currentStage)
    {
        forwardSpeed = MathF.Min(absMaxSpeed, startSpeed * (currentStage * speedIncreasePerStage + 1));
    }

    [Header("SlowPickup")] 
    [SerializeField] private float slowTime = 3f;
    [SerializeField] private float slowFactor = 0.4f;
    Coroutine slowCoroutine;
    float originalSpeed;

    public void ApplySlow()
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            forwardSpeed = originalSpeed;
        }
        slowCoroutine = StartCoroutine(SlowDownCoroutune());
    }

    IEnumerator SlowDownCoroutune()
    {
        originalSpeed = forwardSpeed;
        forwardSpeed *= slowFactor;
        
        yield return new WaitForSeconds(slowTime);
        
        forwardSpeed = originalSpeed;
        slowCoroutine = null;
    }

    Coroutine speedCoroutine;
    public void ApplySpeedBoost(float speedMultiplier, float duration)
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
            forwardSpeed = originalSpeed;
        }
        speedCoroutine = StartCoroutine(SpeedCoroutine(speedMultiplier, duration));
    }

    IEnumerator SpeedCoroutine(float speedMultiplier, float duration)
    {
        originalSpeed = forwardSpeed;
        forwardSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalSpeed;
        speedCoroutine = null;
    }
}