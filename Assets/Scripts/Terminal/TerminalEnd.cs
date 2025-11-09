using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class TerminalEnd : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] GameObject lines;
    [SerializeField] GameObject userLine;
    [SerializeField] GameObject prefabLine;
    [SerializeField] TextMeshProUGUI line;
    [SerializeField] TextMeshProUGUI caret;
    [SerializeField] int sizeText;
    [SerializeField] AudioClip musicEnd;

    float caretTimer = 0;
    [SerializeField] float caretTime = 0.5f;

    bool canType = false;

    public UnityEvent onEndTerminal = new UnityEvent();

    private void Start()
    {
        StartEndTerminal();
    }

    public void StartEndTerminal()
    {
        AudioManager.Instance.PlayMusic(musicEnd);

        StartCoroutine(End());
    }

    void Update()
    {
        if (!canType) return;

        caretTimer += Time.deltaTime;

        if (caretTimer > caretTime)
        {
            caret.text = caret.text == "" ? "_" : "";
            caretTimer = 0;
        }
    }

    IEnumerator AddLineWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddLine(text);
    }

    void AddLine(string text)
    {
        GameObject newLine = Instantiate(prefabLine);
        newLine.transform.SetParent(lines.transform);
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = text;
        newLine.GetComponentInChildren<RectTransform>().transform.localScale = Vector3.one;
        newLine.GetComponentInChildren<TextMeshProUGUI>().fontSize = sizeText;

        userLine.transform.SetAsLastSibling();
    }

    void ReplaceLastLine(string text)
    {
        Transform gameObject = lines.transform.GetChild(lines.transform.childCount - 2);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    IEnumerator End()
    {

        yield return Wait(1f);
        AddLine("[SYS] Autorisation : Minny - niveau : Admin");
        yield return Wait(3f);
        AddLine("[ERROR] Tentative de supprimer Security.exe, l'action n'est pas réalisable");
        yield return Wait(5f);
        AddLine("[SYS] Bypass system security, user : Minny");
        yield return Wait(4f);
        ReplaceLastLine("[ERROR] Vous tentez de supprimer Security.exe");
        yield return Wait(4f);
        ReplaceLastLine("[LOG] Security.exe a été supprimé");
        yield return Wait(4f);
        AddLine("[ALERT] Vous essayez de supprimer des composants essentiels au fonctionnement de l'ordinateur");
        yield return Wait(6f);
        AddLine("[ERROR] Render.exe supprimé, l'ordinateur a besoin de réparation");
        yield return Wait(1f);
        AddLine("[LOG] Suppression de Render.exe succès");
        yield return Wait(3f);
        AddLine("C'était amusant...");
        yield return Wait(3f);
        AddLine("[LOG] Suppression de Taskbar.exe");
        yield return Wait(1f);
        AddLine("Mais...");
        yield return Wait(3f);
        AddLine("[LOG] Suppresion de Settings.exe");
        yield return Wait(1f);
        AddLine("Il faut aller de l'avant non ?");
        yield return Wait(2f);
        AddLine("J'ai bien aimé t'accompagner !");
        yield return Wait(2f);
        AddLine("Mais bon, c'est que dans ta mémoire tout ça tu sais");
        yield return Wait(3f);
        AddLine("Je pense pas que c'était aussi beau avant...");
        yield return Wait(3f);
        AddLine("[LOG] Suppresion de Sound.exe");

        // STOP MUSIQUE
        AudioManager.Instance.StopMusic();

        yield return Wait(2f);
        AddLine("Mais tu sais");
        yield return Wait(1.3f);
        AddLine("[LOG] Suppresion de Minny.exe");
        yield return Wait(4f);
        AddLine("[ERROR] Minny.exe a retourné une erreur en s'arrêtant");
        yield return Wait(3f);
        ReplaceLastLine("[ERROR] Minny.exe a retourné une erreur en s'arrêtant : Byeeee :)");

        onEndTerminal.Invoke();
    }

    WaitForSeconds Wait(float time)
    {
        return new WaitForSeconds(time);
    }
}
