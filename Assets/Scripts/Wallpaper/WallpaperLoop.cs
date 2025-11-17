using UnityEngine;

public class WallpaperLoop : MonoBehaviour
{

    [SerializeField] private Transform pointsToMove;

    void Start()
    {
        AssistantBehaviour.instance.onMoveFinish.AddListener(onEndWalk);
        AssistantBehaviour.instance.MoveTo(pointsToMove.GetChild(0).position);
    }

    void onEndWalk()
    {
        AssistantBehaviour.instance.LookAt(Vector3.zero);
    }

    void Update()
    {
        
    }
}
