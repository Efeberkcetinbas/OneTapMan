using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnRewardedAward,OnRewardedAward);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnRewardedAward,OnRewardedAward);
    }

    private void OnRewardedAward()
    {
        if (remainingCharacters.Count == 0)
        {
            InitializeRemainingCharacters();
        }

        ActivateRandomCharacter();
    }

    [SerializeField] private List<GameObject> characters; // List of character GameObjects

    private Queue<GameObject> remainingCharacters;

    private void Start()
    {
        InitializeRemainingCharacters();
    }

   

    private void InitializeRemainingCharacters()
    {
        remainingCharacters = new Queue<GameObject>(ShuffleList(characters));
    }

    private void ActivateRandomCharacter()
    {
        // Deactivate all characters first
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Activate the selected character
        GameObject selectedCharacter = remainingCharacters.Dequeue();
        selectedCharacter.SetActive(true);

        Debug.Log($"Activated: {selectedCharacter.name}"); // Optional: Log the activated character
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>(list);
        for (int i = 0; i < shuffledList.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledList.Count);
            T temp = shuffledList[i];
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }
        return shuffledList;
    }
}
