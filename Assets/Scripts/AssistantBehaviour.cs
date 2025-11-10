using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistantBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text speechBoxText;
    [SerializeField] private RectTransform speechBox;
    public AssistantState currentAssistantState;
    
    public Action<AssistantState> OnStateChange;
    
    public static AssistantBehaviour instance;

    public Animator anim;

    public Transform minny;

    private Coroutine typingCoroutine;
    void Start()
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

    public void LookAt(Vector3 pos)
    {
        minny.LookAt(pos);
        minny.rotation = Quaternion.Euler(0, minny.rotation.eulerAngles.y, 0);
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
        }

    }

    public void MoveTo(Vector3 pos)
    {
        ChangeState(AssistantState.Running);
        transform.DOMove(pos, 3f).SetEase(Ease.InOutSine).OnComplete( () => { ChangeState(AssistantState.Idle); });
    }

    public void SetAndPrintText(string text)
    {
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

    private IEnumerator TypeText(string fullText)
    {
        speechBoxText.text = fullText;
        speechBoxText.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            speechBoxText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(.05f);
        }

        ChangeState(AssistantState.Idle);
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
