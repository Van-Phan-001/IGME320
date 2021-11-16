using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler
{
    [Header("Properties")]
    [SerializeField][Range(0, 1.0f)] private float dampingSpeed = .05f; //applies a dampened delay to movement

    private RectTransform draggingObj;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        draggingObj = transform as RectTransform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObj, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            draggingObj.position = Vector3.SmoothDamp(draggingObj.position, globalMousePos, ref velocity, dampingSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
