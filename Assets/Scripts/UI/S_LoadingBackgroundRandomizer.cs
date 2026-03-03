using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_LoadingBackgroundRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgrounds;
    private Sprite currentBackground;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        currentBackground = backgrounds[UnityEngine.Random.Range(0, backgrounds.Count)];
        image.sprite = currentBackground;
    }
}
