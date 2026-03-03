using System;
using UnityEngine;

public class S_LoadingSpinner : MonoBehaviour
{
    [Header("Spinner Settings")]
    [SerializeField] private float spinSpeed = 360f;
    
    private RectTransform rectTransform;
    private bool isSpinning = false;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (!rectTransform)
        {
            Debug.LogError("LoadingSpinner requires a RectTransform component!");
            enabled = false;
        }
    }

    private void OnEnable()
    {
       isSpinning = true;
    }

    private void OnDisable()
    {
        isSpinning = false;
    }

    private void Update()
    {
        if (isSpinning && rectTransform)
        {
            float rotationAmount = spinSpeed * Time.deltaTime;
            
            rectTransform.Rotate(0, 0, rotationAmount);
        }
    }
}
