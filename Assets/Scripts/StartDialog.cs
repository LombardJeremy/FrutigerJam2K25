using System;
using System.Collections;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    
    [SerializeField] AssistantBehaviour assistant;

    bool done = false;

    private bool canDialogue = true;

    private void Start()
    {
        GameManager.instance.OnGameStateChanged.AddListener(OnStateGameManager);
    }

    private void Update()
    {
        if (GameManager.instance._taskBarUnlocked && canDialogue)
        {
            canDialogue = false;
            StartCoroutine(TaskBarDialog());
        }
    }

    private void OnStateGameManager(GameState state)
    {
        if (state == GameState.MainOS && !done)
        {
            done = true;
            StartCoroutine(Dialog());
        }
    }

    IEnumerator Dialog()
    {
        AssistantBehaviour.instance.onStartDialog.Invoke();
        assistant.LookAt(Vector3.zero);
        assistant.SetAndPrintText("");
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Start);
        yield return new WaitForSeconds(12.44f);
        assistant.SetAndPrintText("Hellooooooo ça fait longtemps non ?");
        yield return new WaitForSeconds(5f);
        assistant.SetAndPrintText("Hmmmmm cette endroit est un peu en ruine, il faudrait réparer tout ceci");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tu pourrais m'aider... Mais je viens de voir que le driver pour la souris est cassé");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tiens, je vais lancer ta souris, ce sera simple comme bonjour avec cette solution !");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Vise avec les flèches directionnelles et tire avec la barre espace.");
        yield return new WaitForSeconds(14f);
        assistant.SetAndPrintText("Appuis a nouveau sur la barre espace lorsque ta souris est sur l'element souhaité !");
        yield return new WaitForSeconds(14f);
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Idle);
        AssistantBehaviour.instance.onFinishDialog.Invoke();

        // END dialog start
    }
    
    IEnumerator TaskBarDialog()
    {
        AssistantBehaviour.instance.onStartDialog.Invoke();
        assistant.LookAt(Vector3.zero);
        assistant.SetAndPrintText("");
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Speakin);
        yield return new WaitForSeconds(12.44f);
        assistant.SetAndPrintText("Hellooooooo ça fait longtemps non ?");
        yield return new WaitForSeconds(5f);
        assistant.SetAndPrintText("Hmmmmm cette endroit est un peu en ruine, il faudrait réparer tout ceci");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tu pourrais m'aider... Mais je viens de voir que le driver pour la souris est cassé");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tiens, je vais lancer ta souris, ce sera simple comme bonjour avec cette solution !");
        yield return new WaitForSeconds(10f);
        AssistantBehaviour.instance.onFinishDialog.Invoke();
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Idle);
        assistant.SetAndPrintText("Vise avec les flèches directionnelles et tire avec la barre espace.");
        yield return new WaitForSeconds(14f);
        assistant.SetAndPrintText("Appuis a nouveau sur la barre espace lorsque ta souris est sur l'element souhaité !");
        yield return new WaitForSeconds(14f);
        

        // END dialog
    }

}
