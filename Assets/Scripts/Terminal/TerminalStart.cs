using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalStart : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] GameObject lines;
    [SerializeField] GameObject userLine;
    [SerializeField] GameObject prefabLine;
    //[SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI line;
    [SerializeField] TextMeshProUGUI caret;

    [SerializeField] List<string> startTexts;

    float caretTimer = 0;
    [SerializeField] float caretTime = 0.5f;

    int indexWordToSay = 0;
    [SerializeField] List<string> wordsToSay;

    bool canType = true;

    public UnityEvent onEndTerminal = new UnityEvent();
    
    public CanvasGroup canvasGroup;

    void Start()
    {

        for (int i = 0; i < startTexts.Count; i++)
        {
            StartCoroutine(AddLineWithDelay(startTexts[i], 1f * i + Random.value/2f));
        }
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


        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnEnter(line.text);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (line.text.Length > 0)
                    line.text = line.text.Substring(0, line.text.Length - 1);
            }
            else { 
                foreach (char c in Input.inputString)
                {
                    line.text += c;
                }
            }
            caretTimer = 0;
            caret.text = "";
        }
    }

    void OnEnter(string userInput)
    {
        AddLine("C:\\System32>" + line.text);

        if (userInput == wordsToSay[indexWordToSay])
        {

            if (indexWordToSay == 0)
            {
                StartCoroutine(OnStartCommand());
            }

            indexWordToSay++;

            if (indexWordToSay > wordsToSay.Count-1)
            {
                OnLastCommand();
            }
        }
        line.text = "";
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
        newLine.transform.localScale = Vector3.one;
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = text;

        userLine.transform.SetAsLastSibling();
    }

    void ReplaceLastLine(string text)
    {
        Transform gameObject = lines.transform.GetChild(lines.transform.childCount-2);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    IEnumerator OnStartCommand()
    {
        canType = false;
        AddLine("Starting OS");
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f * (i+1));
            string newText = "Starting OS";
            for (int j = 0; j < i+1; j++) newText += ".";
            ReplaceLastLine(newText);
        }

        AddLine("Hey je suis Minny, je suis ton assistant ici");
        yield return new WaitForSeconds(1f);
        AddLine("Oh wow, c'est un peu vide ici, tu peux taper la commande : enable-render");
        yield return new WaitForSeconds(2f);
        AddLine("Cela permettra de rendre tout ceci un peu plus vivant...");
        yield return new WaitForSeconds(1f);

        canType = true;
    }

    void OnLastCommand()
    {
        userLine.SetActive(false);
        StartCoroutine(OnLastCommandCoroutine());
    }

    IEnumerator OnLastCommandCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        AddLine("render card : enabled");
        AddLine("render card starting");
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f * (i + 1));
            string newText = "render card starting";
            for (int j = 0; j < i + 1; j++) newText += ".";
            ReplaceLastLine(newText);
        }
        yield return new WaitForSeconds(0.3f);
        onEndTerminal.Invoke();
        EndIntro();
    }

    public void EndIntro()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, 1f);
        GameManager.instance.ChangeState(GameState.MainOS);
        canvasGroup.GetComponentInParent<Canvas>().enabled = false;
    }

}
