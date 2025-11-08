using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] InteractibleManager intManager;

    [Header("Params Thrower")]
    [SerializeField] Rigidbody2D objectToThrow;
    [SerializeField] float speedRotation = 100;


    [Header("Params Shoot")]
    [SerializeField] float speedShoot = 400f;
    [SerializeField] float friction = 2f;

    bool hasObject = true;
    bool objectReturn = false;
    bool repositioning = false;

    float posXbase = 0;

    void Start()
    {
        posXbase = objectToThrow.transform.localPosition.x;
    }

    void Update()
    {
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
                    objectToThrow.linearVelocity = (transform.position - objectToThrow.transform.position).normalized * speedShoot;
                }
            }
            else
            {
                if (Vector2.Distance(objectToThrow.transform.position, transform.position) < posXbase)
                {
                    objectReturn = false;
                    repositioning = true;
                    objectToThrow.linearVelocity = Vector2.zero;
                    // Dotween pour revenir à la pos
                    objectToThrow.transform.DOLocalMoveX(posXbase, 0.3f).SetEase(Ease.InOutCirc).OnComplete( () => { hasObject = true; repositioning = false; });
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
            intManager.IsCollidingWithElements(objectToThrow.position);
        }
    }

    private void HasObjectUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -speedRotation * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, speedRotation * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        hasObject = false;
        objectToThrow.linearVelocity = transform.rotation.normalized * new Vector2(speedShoot, 0);
    }
}
