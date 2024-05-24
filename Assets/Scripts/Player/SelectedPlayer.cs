using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayer : MonoBehaviour
{
    private Animator animator;

    [Header("EATING")]
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Color eatColor;
    private Color defaultColor;

    private WaitForSeconds waitForSeconds;


    private void Start()
    {
        animator = GetComponent<Animator>();
        defaultColor=skinnedMeshRenderer.material.color;
        waitForSeconds=new WaitForSeconds(.25f);
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlayerEat,OnPlayerEat);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerEat,OnPlayerEat);
    }

    private void OnPlayerEat()
    {
        animator.SetTrigger("Eat");
        StartCoroutine(EatEffect());
    }


    private IEnumerator EatEffect()
    {
        skinnedMeshRenderer.material.color=eatColor;
        yield return waitForSeconds;
        skinnedMeshRenderer.material.color=defaultColor;
    }
}
