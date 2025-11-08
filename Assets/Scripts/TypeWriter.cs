using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypeWriter : MonoBehaviour
{

    [SerializeField] float timePerChar = 0.05f;
    [SerializeField] TextMeshProUGUI tmp;

    public string msg = string.Empty;
    public string currentMsg = string.Empty;


    private float timer = 0;

    private bool reverse = false;

    private bool running = false;

    public UnityEvent onReverseEnd = new UnityEvent();
    public UnityEvent onNormalEnd = new UnityEvent();

    void Update()
    {
        if (!running) return;

        timer += Time.deltaTime;

        if (timer > timePerChar)
        {
            timer = 0;
            
            if (reverse)
            {
                if (currentMsg.Length > 0) currentMsg = currentMsg.Substring(0, currentMsg.Length - 1);

                if (currentMsg.Length == 0)
                {
                    running = false;
                    reverse = false;
                    timer = 0;
                    onReverseEnd.Invoke();
                }
            }
            else
            {
                currentMsg = msg.Substring(0, currentMsg.Length + 1);

                if (currentMsg.Length == msg.Length)
                {
                    running = false;
                    timer = 0;
                    onNormalEnd.Invoke();
                }
            }

        }
    }

    void NewMessage(string newMessage)
    {
        msg = newMessage;
        reverse = true;
        running = true;

        onReverseEnd.AddListener( () => { reverse = false; running = true; } );
    }

    void Erase()
    {
        running = false;
        reverse = true;
    }
}
