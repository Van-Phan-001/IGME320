using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler
{
    [Header("Properties")]
    [SerializeField][Range(0, 1.0f)] private float dampingSpeed = .05f; //applies a dampened delay to movement

    private RectTransform draggingObj;
    private Vector3 velocity = Vector3.zero;

    private IdeaManager ideaManager;
    private void Awake()
    {
        draggingObj = transform as RectTransform;
    }

    private void Start()
    {
        ideaManager = IdeaManager.Instance;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObj, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            draggingObj.position = Vector3.SmoothDamp(draggingObj.position, globalMousePos, ref velocity, dampingSpeed);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log($"This: {this.name}, Other: {collision.transform.name}");
    //    //instantiate obj on collision if the combo exist

    //    //ideaManager.CheckCombo(this.name, collision.transform.name);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"This: {this.name}, Other: {collision.transform.name}");
        ideaManager.CheckCombo(this.name, collision.transform.name);
    }
}
