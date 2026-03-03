using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_Module : MonoBehaviour
{
    [SerializeField] private GameObject basePlane;
    public GameObject BasePlane => basePlane;

    private List<GameObject> interactables = new List<GameObject>();


    private void GetAllInteractables()
    {
        var allInteractables = GetComponentsInChildren<S_Interactable>(true);
        foreach (var interactable in allInteractables)
        {
            var go = interactable.gameObject;
            interactables.Add(go);
        }

        var allPickups = GetComponentsInChildren<S_Pickup>(true);
        foreach (var pickup in allPickups)
        {
            var go = pickup.gameObject;
            interactables.Add(go);
        }
    }

    public void ResetModule()
    {
        interactables.Clear();
        GetAllInteractables();
        foreach (var interactable in interactables)
        {
            if (!interactable.activeInHierarchy)
            {
                interactable.SetActive(true);
            }
        }
    }

    public Material batchMat;

    public void CombineMeshes()
    {
        Debug.Log("Combining Meshes");
        //------------------------------------------------------------
        var allMeshes = GetComponentsInChildren<MeshRenderer>();
        List<GameObject> batchingMats = new List<GameObject>();
        Debug.Log($"Found {allMeshes.Length} Meshes on Obj: {this.name}");
        foreach (var mr in allMeshes)
        {
            if (mr.sharedMaterial == batchMat)
            {
                //Debug.Log($"Found Mesh with right Mat \n");
                batchingMats.Add(mr.gameObject);
            }
        }
        StaticBatchingUtility.Combine(batchingMats.ToArray(), gameObject);
        //------------------------------------------------------------
    }
}