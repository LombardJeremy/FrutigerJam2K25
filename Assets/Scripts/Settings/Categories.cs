using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Categories : MonoBehaviour
{
    [SerializeField] Transform childs;

    [SerializeField] Slider soundSlider;

    [SerializeField] TextMeshProUGUI textUpdate;
    [SerializeField] Transform buttonUpdate;

    [Header("SOUNDS")]
    public AudioSource audioSource;
    public AudioClip success;

    public MainSceneManager mainSceneManager;

    [Header("Background")]
    [SerializeField] Carousel carouselBackground;
    [SerializeField] GameObject fxBackground;
    [SerializeField] TextMeshProUGUI textButtonFxBackground;
    [SerializeField] bool fxActivated = true;
    [SerializeField] string textFxActivated;
    [SerializeField] string textFxDeactivated;

    bool loadingOS = false;

    public bool loading = false;

    int sec = 0;

    // 4 Mise � jour
    // 3 background
    // 2 Son
    // 1 Date 
    // 0 System Info

    void Start()
    {
        DateTime date = DateTime.Parse(TaskBarManager.instance.GetDate());
        childs.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>()[1].text = date.Day.ToString();
        childs.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>()[3].text = date.Month.ToString();
        childs.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>()[5].text = date.Year.ToString();

        if (fxActivated) textButtonFxBackground.text = textFxActivated;
        else textButtonFxBackground.text = textFxDeactivated;

        fxBackground.SetActive(fxActivated);

    }

    void Update()
    {
        if (loading) return;

        switch(sec)
        {
            case 1:
                DateInput();
                break;
            case 2:
                SoundInput(); break;
            case 3:
                BackgroundInput(); break;
            case 4:
                UpdateForOS(); break;
        }
    }

    private void BackgroundInput()
    {
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            fxActivated = !fxActivated;

            if (fxActivated) textButtonFxBackground.text = textFxActivated;
            else textButtonFxBackground.text = textFxDeactivated;

            fxBackground.SetActive(fxActivated);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int index = carouselBackground.GetSelectedIndex();
            carouselBackground.Right();
            int diff = carouselBackground.GetSelectedIndex() - index;

            //if (diff != 0)
            //    audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            int index = carouselBackground.GetSelectedIndex();
            carouselBackground.Left();
            int diff = carouselBackground.GetSelectedIndex() - index;

            //if (diff != 0)
            //    audioSource.Play();
        }


    }

    void UpdateForOS()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !loadingOS)
        {
            StartCoroutine(UpdateOSCoroutine());
        }
    }

    IEnumerator UpdateOSCoroutine()
    {
        loadingOS = true;


        mainSceneManager.asMouse = true;
        buttonUpdate.DOLocalMoveX(10f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);

        textUpdate.text = "Chargement de la mise à jour";
        for (int i = 0; i < 3; i++)
        {
            textUpdate.text += ".";
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0.5f);

        textUpdate.text = "Installation en cours";
        for (int i = 0; i < 3; i++)
        {
            textUpdate.text += ".";
            yield return new WaitForSeconds(1f);
        }

        textUpdate.text = "Mise à jour installé ! Veuillez patienter";

        yield return new WaitForSeconds(2f);

        textUpdate.text = "Système à jour";
        buttonUpdate.DOKill();
        buttonUpdate.DOLocalMoveX(0, 0.5f);

        audioSource.clip = success;
        audioSource.Play();

        PopupWindow.Instance.SetPopup("Souris activé !");
        buttonUpdate.GetComponent<Image>().DOFade(0, 0.5f);
        buttonUpdate.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 0.5f);

        GameManager.instance.minesweeper.SetActive(true);
    }


    void SoundInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundSlider.value += 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            soundSlider.value -= 0.1f;
        }
    }

    void DateInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            childs.GetChild(sec).GetComponent<Carousel>().Right();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            switch(childs.GetChild(sec).GetComponent<Carousel>().GetSelectedIndex())
            {
                case 0:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text) + 1, 1, 30)).ToString();
                    break;
                case 1:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text) + 1, 1, 12)).ToString();
                    break;
                case 2:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text) + 1, 0, 2025)).ToString();
                    break;
            }

            TaskBarManager.instance.SetDate(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text + "/" + childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text + "/" + childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (childs.GetChild(sec).GetComponent<Carousel>().GetSelectedIndex())
            {
                case 0:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text) - 1, 1, 30)).ToString();
                    break;
                case 1:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text) - 1, 1, 12)).ToString();
                    break;
                case 2:
                    childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text = (Mathf.Clamp(int.Parse(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text) - 1, 0, 2025)).ToString();
                    break;
            }

            TaskBarManager.instance.SetDate(childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[1].text + "/" + childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[3].text + "/" + childs.GetChild(sec).GetComponentsInChildren<TextMeshProUGUI>()[5].text);
        }
    }

    public void HideOthers(int selected)
    {
        sec = selected;

        for (int i = 0; i < childs.childCount; i++)
        {
            if (i != selected) childs.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
            else childs.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
        }
    }

}
