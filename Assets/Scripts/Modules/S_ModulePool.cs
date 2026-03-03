using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using Unity.VisualScripting;
using System.Linq;

public class S_ModulePool : MonoBehaviour
{
    private static GameObject activeHolder;
    private static GameObject poolHolder;

    // All Clones (Value) that are made with the original Prefab (Key)
    private static Dictionary<GameObject, ObjectPool<GameObject>> objectPool;

    // The Original Prefab (Value) of that clone (Key)
    private static Dictionary<GameObject, GameObject> cloneToPrefabMap;


    public static S_ModulePool Instance;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        objectPool = new Dictionary<GameObject, ObjectPool<GameObject>>();
        cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        activeHolder = new GameObject("ModuleHolder");
        poolHolder = new GameObject("PoolHolder")
        {
            transform =
            {
                position = new Vector3(0, 0, -3000)
            }
        };

        poolHolder.SetActive(false);
    }

    private static void CreatePool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
        );
        objectPool.Add(prefab, pool);
    }

    private static GameObject CreateObject(GameObject prefab)
    {
        var obj = Instantiate(prefab, poolHolder.transform.position, Quaternion.identity);
        obj.transform.SetParent(poolHolder.transform, false);

        if (obj.GetComponent<S_Module>())
        {
            obj.GetComponent<S_Module>().CombineMeshes();
        }

        cloneToPrefabMap[obj] = prefab;

        return obj;
    }

    private static void OnGetObject(GameObject obj)
    {
        obj.transform.SetParent(activeHolder.transform, false);
        obj.SetActive(true);

        if (obj.gameObject.GetComponent<S_Module>())
        {
            obj.gameObject.GetComponent<S_Module>().ResetModule();
        }
    }

    private static void OnReleaseObject(GameObject obj)
    {
        //obj.SetActive(false);
        obj.transform.SetParent(poolHolder.transform);
    }

    private static void OnDestroyObject(GameObject obj)
    {
        cloneToPrefabMap.Remove(obj);
    }


    public static void PreloadPrefab(GameObject prefab)
    {
        if (!objectPool.ContainsKey(prefab))
        {
            CreatePool(prefab, poolHolder.transform.position, prefab.transform.rotation);
        }


        var pool = objectPool[prefab];

        var temp = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            var clone = objectPool[prefab].Get();
            temp.Add(clone);
            cloneToPrefabMap[clone] = prefab;
        }

        foreach (var clone in temp)
        {
            ReturnObjectToPool(clone);
        }
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (!objectPool.ContainsKey(objectToSpawn))
        {
            CreatePool(objectToSpawn, poolHolder.transform.position, spawnRotation);
        }

        var obj = objectPool[objectToSpawn].Get();
        obj.transform.position = spawnPosition;
        obj.transform.rotation = spawnRotation;

        return obj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        if (!cloneToPrefabMap.TryGetValue(obj, out var prefab))
            return;

        if (obj.transform.parent != poolHolder.transform)
        {
            obj.transform.SetParent(poolHolder.transform);
        }

        if (objectPool.TryGetValue(prefab, out var pool))
        {
            pool.Release(obj);

        }
    }
}