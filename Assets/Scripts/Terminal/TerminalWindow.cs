using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TerminalWindow : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] GameObject lines;
    [SerializeField] GameObject userLine;
    [SerializeField] GameObject prefabLine;
    [SerializeField] TextMeshProUGUI line;
    [SerializeField] TextMeshProUGUI caret;
    [SerializeField] int sizeText;

    [SerializeField] List<string> startTexts;

    [SerializeField] List<string> onDoneTexts;

    float caretTimer = 0;
    [SerializeField] float caretTime = 0.5f;

    [SerializeField] List<string> commandsToSay;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip typeSound;
    [SerializeField] AudioClip linePop;

    List<bool> doneCommands = new List<bool>(); 

    bool canType = true;

    bool end = false;

    public UnityEvent onEndTerminal = new UnityEvent();
    public List<UnityEvent> onDoneCommand = new List<UnityEvent>();

    void Start()
    {

        for (int i = 0; i < commandsToSay.Count; i++)
        {
            doneCommands.Add(false);
        }

        for (int i = 0; i < startTexts.Count; i++)
        {
            StartCoroutine(AddLineWithDelay(startTexts[i], 1f * i + Random.value / 2f));
        }
    }

    void Update()
    {
        if (end) return;

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
            else
            {
                foreach (char c in Input.inputString)
                {
                    line.text += c;
                }

                audioSource.Stop();
                audioSource.clip = typeSound;
                audioSource.Play();
            }
            caretTimer = 0;
            caret.text = "";
        }
    }

    void OnEnter(string userInput)
    {

        if (commandsToSay.Contains(userInput))
        {
            AddLine("C:\\System32>" + line.text);

            StartCoroutine(OnDoneCommand(commandsToSay.IndexOf(userInput)));

            
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
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = text;
        newLine.GetComponentInChildren<RectTransform>().transform.localScale = Vector3.one;
        newLine.GetComponentInChildren<TextMeshProUGUI>().fontSize = sizeText;

        userLine.transform.SetAsLastSibling();

        audioSource.Stop();
        audioSource.clip = linePop;
        audioSource.Play();
    }

    void ReplaceLastLine(string text)
    {
        Transform gameObject = lines.transform.GetChild(lines.transform.childCount - 2);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    IEnumerator OnDoneCommand(int index)
    {
        canType = false;

        yield return new WaitForSeconds(1f);
        AddLine(onDoneTexts[index]);

        //foreach (string text in onDoneTexts[index])
        //{
        //    yield return new WaitForSeconds(1f);
        //    AddLine(text);
        //}

        doneCommands[index] = true;
        if (index < onDoneCommand.Count-1 && onDoneCommand[index] != null) onDoneCommand[index].Invoke();
        canType = true;

        int done = 0;
        for (int i = 0; i < commandsToSay.Count; i++)
            if (doneCommands[i]) { done++; }

        if (done >= commandsToSay.Count) OnLastCommand();
    }

    void OnLastCommand()
    {
        userLine.SetActive(false);

        StartCoroutine(OnLastCommandCoroutine());
    }

    IEnumerator OnLastCommandCoroutine()
    {
        // ICI FAUT FAIRE LE TRUC POUR LES DERNIERS TEXTS
        yield return new WaitForSeconds(0.5f);
        AddLine("you found all the secrets");
        yield return new WaitForSeconds(0.5f);
        end = true;

        onEndTerminal.Invoke();
    }

}
