using UnityEngine;

public class WallpaperLoop : MonoBehaviour
{

    [SerializeField] private Transform pointsToMove;

    int indexPoint = 0;

    void Start()
    {
        AssistantBehaviour.instance.onMoveFinish.AddListener(onEndWalk);
        AssistantBehaviour.instance.MoveTo(pointsToMove.GetChild(indexPoint).position);
    }

    void onEndWalk()
    {
        AssistantBehaviour.instance.LookAt(Vector3.zero);
        indexPoint++;

        if (indexPoint < pointsToMove.childCount) AssistantBehaviour.instance.MoveTo(pointsToMove.GetChild(indexPoint).position);
    }

    void Update()
    {
        
    }
}
