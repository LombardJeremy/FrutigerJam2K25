using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistantBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text speechBoxText;
    [SerializeField] private RectTransform speechBox;
    
    private Coroutine typingCoroutine;
    void Start()
    {
        //Play StartAnimation
        SetAndPrintText("HHHHHHHHHHHHHWOWOWOOWOWOOWOWOOWOWO");
        MoveTo(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        //Choose a random thing to do if player don't interact with it
    }

    public void MoveTo(Vector3 pos)
    {
        transform.DOMove(pos, 3f).SetEase(Ease.InOutSine);
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
    }
}
