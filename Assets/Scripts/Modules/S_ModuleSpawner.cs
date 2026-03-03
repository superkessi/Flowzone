using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ModuleSpawner : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private UnityEngine.UI.Slider progressBar;
    //private bool finishedPreloading = false;

    [Header("Stages")] 
    [SerializeField] private GameObject fillerModule;
    [SerializeField] private StageData tutorialStages;
    [SerializeField] private List<StageData> stages;
    private StageData currentStage;

    private int renderDistanceSide = 1;
    private int renderDistanceForward = 1;
    
    private Coroutine updateModulesCoroutine;

    private S_Player player;

    private float moduleLength;
    private float moduleWidth;

    private List<GameObject> activeModules = new List<GameObject>();
    private readonly HashSet<Vector2Int> moduleGridPositions = new HashSet<Vector2Int>();

    private Vector2Int oldPlayerGridPos = new Vector2Int(0, 0);

    private const string TutorialKey = "TutorialPlayed";
    //public bool spawnTutorial = true;
    
    public bool randomizeStages = false;
   // private int tutorialIndex = 0;
    
    private void Start()
    {
        Debug.Log($"Tutorial Key thingy : {PlayerPrefs.GetInt(TutorialKey)}");
        
        player = FindFirstObjectByType<S_Player>();
        oldPlayerGridPos = GetGridPos(player.Position);
        
        foreach (var stage in stages)
        {
            stage.RandomizeStage();
        }
        
        if (PlayerPrefs.GetInt(TutorialKey) == 0)
        {
            currentStage = tutorialStages;
        }
        else
        {
            LoadStage(0);
            var temp = currentStage;
            foreach (var stage in currentStage.stagePrefabs)
            {
                if (stage == fillerModule)
                {
                    temp.stagePrefabs.Remove(stage);
                    Debug.Log("REMOVING DUPLICATE FILLER");
                    break;
                }
            }
            temp.stagePrefabs.Insert(0, fillerModule);
            currentStage = temp;
        }

        GetModuleSize();
    
        loadingScreen.SetActive(true);
        
        StartCoroutine(PreloadModulesCoroutine());
    }

    private void Update()
    {
        var newPlayerGridPos = GetGridPos(player.Position);
        if (oldPlayerGridPos == newPlayerGridPos)
            return;
        oldPlayerGridPos = newPlayerGridPos;

        if (!isSpawningModules)
        {
            if (updateModulesCoroutine == null)
            {
                StopCoroutine(UpdateModulesCoroutine());
            }
            updateModulesCoroutine = StartCoroutine(UpdateModulesCoroutine());
        }
        //UpdateModules();
    }

    private void GetModuleSize()
    {
        var moduleBase = currentStage.stagePrefabs[0].GetComponent<S_Module>().BasePlane;
        var rend = moduleBase.GetComponent<Renderer>();
        moduleLength = rend.bounds.size.z;
        moduleWidth = rend.bounds.size.x;
    }

    Vector2Int GetGridPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - moduleWidth / 2) / moduleWidth) + 1;
        int z = Mathf.FloorToInt(worldPos.z / moduleLength);
        return new Vector2Int(x, z);
    }

    public void LoadStage(int stage)
    {
        player.CurrentStage++;
        if (stage > stages.Count - 1)
            stage = 0;
        currentStage = stages[stage];
        if (randomizeStages)
            currentStage.RandomizeStage();
    }

    private List<StageData> allStages = new List<StageData>();
    private IEnumerator PreloadModulesCoroutine()
    {
        allStages.Clear();
        foreach (var stage in stages)
        {
            allStages.Add(stage);
        }
        
        int totalPrefabs = 0;
        foreach (var stage in allStages)
        {
            totalPrefabs += stage.stagePrefabs.Count;
        }
        
        int preloadedCount = 0;
        
        foreach (var stage in allStages)
        {
            foreach (var prefab in stage.stagePrefabs)
            {
                if (loadingText)
                    loadingText.text = $"Loading {prefab.name}...";
                
                S_ModulePool.PreloadPrefab(prefab);

                preloadedCount++;

                if (progressBar)
                    progressBar.value = (float)preloadedCount / totalPrefabs;

                yield return null;
                
                //yield return new WaitForSeconds(0.1f);
            }
        }

        if (progressBar)
            progressBar.value = 1f;
        
        yield return new WaitForSeconds(0.5f);
        
        if (loadingScreen)
            loadingScreen.SetActive(false);
        
        //finishedPreloading = true;
        InitializeStartModules();
    }
    
    private void InitializeStartModules()
    {
        player.canMove = false;
        var playerGridPos = GetGridPos(player.Position);

        if (PlayerPrefs.GetInt(TutorialKey) == 0)
        {
            for (int z = 0; z < tutorialStages.stagePrefabs.Count; z++)
            {
                for (int x = -renderDistanceSide; x <= renderDistanceSide; x++)
                {
                    Vector2Int gridPos = new Vector2Int(playerGridPos.x + x, playerGridPos.y + z);
                    if (!moduleGridPositions.Contains(gridPos))
                    {
                        var prefab = tutorialStages.stagePrefabs[z];
                        SpawnModuleAt(prefab, gridPos);
                    }
                }
            }
            
            //tutorialIndex = 0;
            
            PlayerPrefs.SetInt(TutorialKey, 1);
            PlayerPrefs.Save();
            
            LoadStage(0);
        }
        else
        {
            for (int z = 0; z <= renderDistanceForward; z++)
            {
                for (int x = -renderDistanceSide; x <= renderDistanceSide; x++)
                {
                    Vector2Int gridPos = new Vector2Int(playerGridPos.x + x, playerGridPos.y + z);

                    if (!moduleGridPositions.Contains(gridPos))
                    {
                        int prefabIndex = gridPos.y % currentStage.stagePrefabs.Count;
                        GameObject prefab = currentStage.stagePrefabs[prefabIndex];
                        SpawnModuleAt(prefab, gridPos);
                    }
                }
            }
        }
        player.canMove = true;
    }

    private float spawnDelay = 0.2f;
    private bool isSpawningModules = false;
    private IEnumerator UpdateModulesCoroutine()
    {
        isSpawningModules = true;
        
        Vector2Int playerGridPos = GetGridPos(player.Position);
        List<Vector2Int> spawnPositions = new List<Vector2Int>();
        
        for(int z = 0; z <= renderDistanceForward; z++)
        {
            for(int x = -renderDistanceSide; x <= renderDistanceSide; x++)
            {
                Vector2Int gridPos = new Vector2Int(playerGridPos.x + x, playerGridPos.y + z);

                if (!moduleGridPositions.Contains(gridPos))
                {
                    spawnPositions.Add(gridPos);
                }
            }
        }

        foreach (var spawnPos in spawnPositions)
        {
            var prefabIndex = playerGridPos.y % currentStage.stagePrefabs.Count;
            var prefab = currentStage.stagePrefabs[prefabIndex];
            SpawnModuleAt(prefab, spawnPos);
            yield return new WaitForSeconds(spawnDelay);
        }
        
        CleanUpModules();
        
        isSpawningModules = false;
    }

    void SpawnModuleAt(GameObject modulePrefab, Vector2Int gridPos)
    {
        Vector3 spawnWorldPos = new Vector3(
            gridPos.x * moduleWidth,
            0,
            gridPos.y * moduleLength + moduleLength / 2
        );
        
        var module = S_ModulePool.SpawnObject(modulePrefab, spawnWorldPos, Quaternion.identity);

        activeModules.Add(module);
        moduleGridPositions.Add(gridPos);
    }

    private void CleanUpModules()
    {
        Vector2Int playerGrid = GetGridPos(player.Position);

        for (int i = activeModules.Count - 1; i >= 0; i--)
        {
            GameObject module = activeModules[i];
            Vector2Int gridPos = GetGridPos(module.transform.position);

            int distanceX = Mathf.Abs(playerGrid.x - gridPos.x);
            int distanceY = playerGrid.y - gridPos.y;

            if (distanceX > renderDistanceSide || distanceY > 0)
            {
                S_ModulePool.ReturnObjectToPool(module);
                activeModules.RemoveAt(i);
                moduleGridPositions.Remove(gridPos);
            }
        }
    }
}