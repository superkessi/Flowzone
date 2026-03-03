using Unity.Cinemachine;
using UnityEngine;

public class S_PlayerCam : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private float startFOV = 70;
    [SerializeField] private float endFOV = 106;
    [SerializeField] private float maxSpeed = 200f;
    [SerializeField] private float smoothSpeed = 5f;

    private S_Player player;

    void Start()
    {
        player = FindFirstObjectByType<S_Player>();
        if (!cam)
            cam = GetComponent<CinemachineCamera>();
    }

    
    void Update()
    {
        if (!player || !cam)
        {
            Debug.Log("Player or Camera not found");
            return;
        }
        
        float t = Mathf.Clamp01(player.CurrentForwardSpeed / maxSpeed);
        float targetFOV = Mathf.Lerp(startFOV, endFOV, t);
        
        cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
    }
    
}