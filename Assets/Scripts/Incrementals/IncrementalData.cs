using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "IncrementalData", menuName = "Data/IncrementalData", order = 4)]
public class IncrementalData : ScriptableObject
{
    //Will be implemented

    public int PressTimePrice;
    public int IncreaseScorePrice;
    public int BuffTimePrice;
    public int increaseScore;

    public float pressTime;

    private string filePath;

    public void SaveDataWithJson()
    {
        filePath=Application.persistentDataPath + "incrementalData.json";
        string jsonData=JsonUtility.ToJson(this);
        File.WriteAllText(filePath,jsonData);
    }

    public void LoadDataWithJson()
    {
        filePath=Application.persistentDataPath + "incrementalData.json";

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

    
    
}
