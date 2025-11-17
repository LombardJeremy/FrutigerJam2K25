using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class AssistantBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text speechBoxText;
    [SerializeField] private RectTransform speechBox;
    public AssistantState currentAssistantState;
    public AudioSource audioSource;
    
    public Action<AssistantState> OnStateChange;
    
    public static AssistantBehaviour instance;

    public Animator anim;

    public UnityEvent onStartDialog = new UnityEvent();
    public UnityEvent onFinishDialog = new UnityEvent();

    List<string> dialogs = new List<string>();
    int currentIndexDialog = 0;
    bool printingDialog = false;

    bool pressedWhilePrinting = false;

    public Transform minny;

    private Coroutine typingCoroutine;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        //Play StartAnimation
        currentAssistantState = AssistantState.Idle;
        
        /*
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, minny.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        speechBox.localPosition = localPoint;
        */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogs.Count > 0)
        {
            if (!printingDialog)
            {
                currentIndexDialog++;
                if (currentIndexDialog > dialogs.Count - 1)
                    OnEndDialog();
                else
                    SetAndPrintText(dialogs[currentIndexDialog]);
            }
            else
            {
                pressedWhilePrinting = true;
            }
        }
    }

    public void LookAt(Vector3 pos)
    {
        minny.LookAt(pos);
        minny.rotation = Quaternion.Euler(0, 90 + minny.rotation.eulerAngles.y, 0);
    }


    public void ChangeState(AssistantState newState)
    {
        switch (currentAssistantState)
        {
            case AssistantState.Speakin:
                anim.SetBool("IsTalking", false);
                break;
            case AssistantState.Running:
                anim.SetBool("IsWalking", false);
                break;
            case AssistantState.ThrowMode:
                anim.SetBool("ThrowingMode", false);
                break;
            
        }

        currentAssistantState = newState;
        OnStateChange?.Invoke(currentAssistantState);

        switch (newState)
        {
            case AssistantState.Speakin:
                anim.SetBool("IsTalking", true);
                break;
            case AssistantState.Running:
                anim.SetBool("IsWalking", true);
                break;
            case AssistantState.ThrowMode:
                anim.SetBool("ThrowingMode", true);
                break;
            case AssistantState.Throw:
                anim.SetTrigger("CursorThrow");
                break;
            case AssistantState.RecieveThrow:
                anim.SetTrigger("CursorRecieved");
                break;
            case AssistantState.Start:
                anim.SetTrigger("Intro");
                break;
            case AssistantState.End:
                anim.SetTrigger("EndCutscene");
                break;
        }

    }

    public void MoveTo(Vector3 pos)
    {
        ChangeState(AssistantState.Running);
        transform.DOMove(pos, 3f).SetEase(Ease.InOutSine).OnComplete( () => { ChangeState(AssistantState.Idle); });
    }

    public void SetAndPrintText(string text)
    {
        pressedWhilePrinting = false;
        speechBoxText.text = text;
        if (!speechBox.gameObject.activeSelf)
        {
            speechBox.gameObject.SetActive(true);
        }
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(text));
        ChangeState(AssistantState.Speakin);
    }

    public void SetDialogsAndPlay(List<string> newDialogs)
    {
        dialogs = newDialogs;
        currentIndexDialog = 0;

        onStartDialog.Invoke();

        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        SetAndPrintText("");
        yield return new WaitForSeconds(0.1f);
        SetAndPrintText(dialogs[currentIndexDialog]);
    }

    private IEnumerator TypeText(string fullText)
    {
        printingDialog = true;
        speechBoxText.text = fullText;
        speechBoxText.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            if (fullText != "") audioSource.Play();
            speechBoxText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(pressedWhilePrinting ? .01f : .05f);
        }

        printingDialog = false;
        ChangeState(AssistantState.Idle);
    }

    private void OnEndDialog()
    {
        dialogs = new List<string>();
        currentIndexDialog = 0;
        speechBoxText.text = "";
        speechBox.gameObject.SetActive(false);
        ChangeState(AssistantState.Idle);
        onFinishDialog.Invoke();
    }

    public enum AssistantState
    {
        Start,
        Idle,
        Speakin,
        Running,
        ThrowMode,
        Throw,
        WaitingForThrow,
        RecieveThrow,
        End
    }
}
