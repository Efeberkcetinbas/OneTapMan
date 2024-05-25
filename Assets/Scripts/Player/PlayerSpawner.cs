using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum SpawnTypes
{
    Food,
    Car,
    Gems,
    Toys,
    Origami,
    Voxel
}

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private SpawnTypes spawnTypes;

    // Serialize the prefabs lists to be assigned in the inspector
    [SerializeField] private List<GameObject> foodPrefabs;
    [SerializeField] private List<GameObject> carPrefabs;
    [SerializeField] private List<GameObject> gemsPrefabs;
    [SerializeField] private List<GameObject> toysPrefabs;
    [SerializeField] private List<GameObject> origamiPrefabs;
    [SerializeField] private List<GameObject> voxelPrefabs;

    private Dictionary<SpawnTypes, List<GameObject>> spawnPrefabs;

    // Define the bounds for the spawn area
    [SerializeField] private Vector3 spawnAreaMin;
    [SerializeField] private Vector3 spawnAreaMax;

    [Header("JUMP")]
    [SerializeField] private PlayerMesh playerMesh;
    [SerializeField] private float jumpPower;
    [SerializeField] private int jumpNumber;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    [Header("DATA")]
    [SerializeField] private GameData gameData;
    private int multiplier=1;

    private void Awake()
    {
        // Initialize the dictionary
        spawnPrefabs = new Dictionary<SpawnTypes, List<GameObject>>
        {
            { SpawnTypes.Food, foodPrefabs },
            { SpawnTypes.Car, carPrefabs },
            { SpawnTypes.Gems, gemsPrefabs },
            { SpawnTypes.Toys, toysPrefabs },
            { SpawnTypes.Origami, origamiPrefabs },
            { SpawnTypes.Voxel, voxelPrefabs }
        };
    }

    private void OnStopTimer()
    {
        // Try to get the prefab list from the dictionary
        if (spawnPrefabs.TryGetValue(spawnTypes, out List<GameObject> prefabs) && prefabs.Count > 0)
        {
            // Select a random prefab from the list
            int randomIndex = Random.Range(0, prefabs.Count);
            GameObject prefabToSpawn = prefabs[randomIndex];

            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
            
            // Instantiate the selected prefab at the spawner's position and rotation
            GameObject spawnedObject=Instantiate(prefabToSpawn, randomPosition, transform.rotation);
            CreateScale();
            spawnedObject.transform.localScale*=multiplier;
            
            spawnedObject.transform.DOLocalRotate(new Vector3(-360,0,0),duration,RotateMode.FastBeyond360).SetRelative(true).SetEase(ease);
            spawnedObject.transform.DOJump(playerMesh.Mouth.position,jumpPower,jumpNumber,duration).OnComplete(()=>{
                EventManager.Broadcast(GameEvent.OnPlayerEat);
                Destroy(spawnedObject);
            });
            
        }
        else
        {
            Debug.LogWarning("No prefabs assigned for the selected spawn type or spawn type is unknown");
        }
    }


    private int CreateScale()
    {
        float clampedTime=Mathf.Clamp(gameData.RoundedTime,0,100);
        multiplier=Mathf.FloorToInt(clampedTime/20)+1;
        return multiplier;
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);

    }

    
}