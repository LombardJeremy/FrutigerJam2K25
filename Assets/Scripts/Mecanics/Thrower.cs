using System;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] InteractibleManager intManager;

    [Header("Params Thrower")]
    [SerializeField] Rigidbody2D objectToThrow;
    [SerializeField] Transform indicator;
    [SerializeField] float speedRotation = 100;


    [Header("Params Shoot")]
    [SerializeField] float speedShoot = 400f;
    [SerializeField] float friction = 2f;
    [SerializeField] float timeWaitShoot = 2f;

    bool hasObject = true;
    bool objectReturn = false;
    bool repositioning = false;
    bool isShooting = false;

    bool isInDialog = false;

    float posXbase = 0;

    float timeNotTouch = 0;
    float timeMaxNotTouch = 2f;

    void Start()
    {
        posXbase = objectToThrow.transform.localPosition.x;
        objectToThrow.gameObject.SetActive(false);

        AssistantBehaviour.instance.onStartDialog.AddListener( () => { isInDialog = true; });
        AssistantBehaviour.instance.onFinishDialog.AddListener(() => { isInDialog = false; });
    }

    private void OnDisable()
    {
        AssistantBehaviour.instance.onStartDialog.RemoveListener( () => { isInDialog = true; });
        AssistantBehaviour.instance.onFinishDialog.RemoveListener(() => { isInDialog = false; });
    }

    void Update()
    {
        if (GameManager.instance.currentGameState != GameState.MainOS) return;
            
        if (isInDialog) return;

        HandleInput();

        if (repositioning) return;

        if (hasObject)
            HasObjectUpdate();
        else
        {

            if (!objectReturn) { 
                objectToThrow.linearVelocity = Vector2.MoveTowards(objectToThrow.linearVelocity, Vector2.zero, friction * Time.deltaTime);

                // Peut le faire avec DOtween
                if (Vector2.Distance(objectToThrow.linearVelocity, Vector2.zero) < 0.1f)
                {
                    objectReturn = true;
                    objectReturn = false;
                    repositioning = true;
                    objectToThrow.linearVelocity = Vector2.zero;
                    //objectToThrow.linearVelocity = (transform.position - objectToThrow.transform.position).normalized * speedShoot;
                    objectToThrow.transform.DOLocalMoveX(0, 2f).SetEase(Ease.InOutCirc).OnComplete(() => { AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.RecieveThrow); hasObject = true; repositioning = false; objectToThrow.gameObject.SetActive(false); });
                }
            }
            else
            {
                if (Vector2.Distance(objectToThrow.transform.position, transform.position) < 0)
                {
                    
                }
            }
        }
    }

    private void HandleInput()
    {
        if (intManager == null) return;
        if (hasObject) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Canvas canvas = objectToThrow.GetComponentInParent<Canvas>();
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, objectToThrow.position);
            bool result = intManager.IsCollidingWithElements(screenPos);
        }
    }

    private void HasObjectUpdate()
    {
        if (isShooting) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -speedRotation * Time.deltaTime);
            AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.ThrowMode);
            timeNotTouch = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, speedRotation * Time.deltaTime);
            AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.ThrowMode);
            timeNotTouch = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            timeNotTouch = 0;
        }
        AssistantBehaviour.instance.LookAt((indicator.position - transform.position).normalized * 100f);

        timeNotTouch += Time.deltaTime;
        if (timeNotTouch > timeMaxNotTouch)
        {
            AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Idle);
        }
        
    }

    private void Shoot()
    {
        AssistantBehaviour.instance.ChangeState(AssistantBehaviour.AssistantState.Throw);
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        isShooting = true;
        yield return new WaitForSeconds(timeWaitShoot);
        isShooting = false;
        hasObject = false;
        objectToThrow.gameObject.SetActive(true);
        objectToThrow.linearVelocity = transform.rotation.normalized * new Vector2(speedShoot, 0);
    }
}
