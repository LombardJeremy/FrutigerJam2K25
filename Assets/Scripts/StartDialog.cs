using System.Collections;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    
    [SerializeField] AssistantBehaviour assistant;

    bool done = false;

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.instance.OnGameStateChanged.AddListener(OnStateGameManager);
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
        assistant.SetAndPrintText("Hellooooooo ça fait longtemps non ?");
        yield return new WaitForSeconds(10f);
        assistant.MoveTo(assistant.transform.position + Vector3.right * 10f);
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Hmmmmm cette endroit est un peu en ruine, il faudrait réparer tout ceci");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tu pourrais m'aider... Mais je viens de voir que le driver pour la souris est cassé");
        yield return new WaitForSeconds(10f);
        assistant.SetAndPrintText("Tiens, je vais lancer ta souris, ce sera simple comme bonjour avec cette solution !");
        yield return new WaitForSeconds(10f);

        // END dialog start
    }

}
