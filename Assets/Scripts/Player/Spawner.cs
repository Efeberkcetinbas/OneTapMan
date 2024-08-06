using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private LevelRequirement levelRequirement;


    private WaitForSeconds waitForSeconds;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStopTimer,OnStopTimer);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);

    }

    private void Start()
    {
        OnNextLevel();
        waitForSeconds=new WaitForSeconds(.1f);
    }

    private void OnNextLevel()
    {
        levelRequirement=FindObjectOfType<LevelRequirement>();
    }
    private void OnStopTimer()
    {
        StartCoroutine(SpawnBallsBasedOnTimer());
        EventManager.Broadcast(GameEvent.OnPlayerEat);
    }


    private IEnumerator SpawnBallsBasedOnTimer()
    {
        int numberOfBalls = GetNumberOfBallsToSpawn(gameData.RoundedTime);
        for (int i = 0; i < numberOfBalls; i++)
        {
            yield return waitForSeconds;
            GameObject ball = ObjectPool.Instance.GetPooledObject();
            EventManager.Broadcast(GameEvent.OnSpawnBall);

            if (ball != null)
            {
                Vector3 randomPosition = GetRandomPositionBetweenTransforms(levelRequirement.spawnPosition1,levelRequirement.spawnPosition2);
                ball.transform.position = randomPosition; // Use the random spawn position
                ball.SetActive(true);
                
            }
        }
    }

    Vector3 GetRandomPositionBetweenTransforms(Transform corner1, Transform corner2)
    {
        Vector3 randomPos = new Vector3(
            Random.Range(corner1.position.x, corner2.position.x),
            Random.Range(corner1.position.y, corner2.position.y),
            Random.Range(corner1.position.z, corner2.position.z)
        );
        return randomPos;
    }

    

    int GetNumberOfBallsToSpawn(float timeValue)
    {
        // Normalize the timeValue to a range of 1 to 10
        int numberOfBalls = Mathf.Clamp(Mathf.CeilToInt(timeValue / 10f), 1, 10);
        return numberOfBalls;
    }
}
