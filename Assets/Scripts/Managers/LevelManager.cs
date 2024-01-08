using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Indexes")]
    public int levelIndex;
    
    public GameData gameData;
    public LevelData levelData;
    public IncrementalData incrementalData;
    public List<GameObject> levels;

    private WaitForSeconds waitForSeconds;

    private void Awake() 
    {
        levelData.LoadDataWithJson();
        incrementalData.LoadDataWithJson();
        LoadLevel();
        waitForSeconds=new WaitForSeconds(.5f);
    }

    
    

    
    private void LoadLevel()
    {


        levelIndex = PlayerPrefs.GetInt("NumberOfLevel");
        if (levelIndex == levels.Count) levelIndex = 0;
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex);
       

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        Debug.Log(levelIndex);
        levels[levelIndex].SetActive(true);
        
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex + 1);
        PlayerPrefs.SetInt("RealNumberLevel", PlayerPrefs.GetInt("RealNumberLevel", 0) + 1);
        levelData.IndexOfLevel++;
        LoadLevel();
        EventManager.Broadcast(GameEvent.OnNextLevel);
        levelData.SaveDataWithJson();
    }
    
    public void RestartLevel()
    {
        //LoadLevel();
        Debug.Log("RESTART LEVEL");
    }

    
    
    
}
