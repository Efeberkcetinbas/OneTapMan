using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public int IndexOfLevel;
    public int score;

    public int challengeIndex;
    public int highScoreChalleIndex;

    private string filePath;

    //2 Solutions for Data Saving
    #region  JSON SAVING
    public void SaveDataWithJson()
    {
        filePath=Application.persistentDataPath + "levelData.json";
        string jsonData=JsonUtility.ToJson(this);
        File.WriteAllText(filePath,jsonData);

    }

    public void LoadDataWithJson()
    {
        filePath=Application.persistentDataPath + "levelData.json";

        if(File.Exists(filePath))
        {
            string jsonData=File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData,this);
        }

        else
        {
            Debug.LogWarning("THERE IS NO SUCH A FILE");
        }

    }

    public void DeleteJsonData()
    {
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Json file Deleted Successfully");
        }
        else
            Debug.Log("No Json Data file found to delete");
    }

    #endregion

    #region  PLAYERPREFS

    public void SaveDataWithPrefs()
    {
        PlayerPrefs.SetInt("IndexOfLevel",IndexOfLevel);
        PlayerPrefs.SetInt("Score",score);
    }

    public void LoadDataWithPrefs()
    {
        IndexOfLevel=PlayerPrefs.GetInt("IndexOfLevel");
        score=PlayerPrefs.GetInt("Score");
    }



    #endregion


}
