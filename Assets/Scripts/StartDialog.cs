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
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Appuis a nouveau sur la barre espace lorsque ta souris est sur l'element souhaité !");
        yield return new WaitForSeconds(5f);
        AssistantBehaviour.instance.onFinishDialog.Invoke();
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Idle);

        // END dialog start
    }
    
    IEnumerator TaskBarDialog()
    {
        AssistantBehaviour.instance.onStartDialog.Invoke();
        assistant.LookAt(Vector3.zero);
        assistant.SetAndPrintText("");
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Speakin);
        assistant.SetAndPrintText("La barre des tâches est de nouveau accessible ! Très bonne nouvelle.");
        yield return new WaitForSeconds(7f);
        assistant.SetAndPrintText("On devrait pouvoir accéder aux paramètres.");
        yield return new WaitForSeconds(6f);
        assistant.SetAndPrintText("Fouille donc dedans, fais comme chez toi!");
        yield return new WaitForSeconds(3f);
        AssistantBehaviour.instance.onFinishDialog.Invoke();
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Idle);
        
        // END dialog
    }

}
