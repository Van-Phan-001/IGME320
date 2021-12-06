using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Properties")]
    [SerializeField][Range(0, 1.0f)] private float dampingSpeed = .05f; //applies a dampened delay to movement
    [SerializeField] private Text text;
    [SerializeField] private Transform hoverImage;
    [SerializeField] public TextMeshProUGUI theHoverText;

    private RectTransform draggingObj;
    private Vector3 velocity = Vector3.zero;
    private bool isDragged = false;
    AudioManager am = AudioManager.Instance;

    private IdeaManager ideaManager;
    private void Awake()
    {
        
        draggingObj = transform as RectTransform;
    }

    private void Start()
    {
        hoverImage.localPosition = new Vector3(10000f, 0, 0);
        ideaManager = IdeaManager.Instance;
    }

    /// <summary>
    /// Drags object around
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObj, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            //Debug.Log($"Dragging {isDragged}");
            am.PlaySound("IdeaMove");
            isDragged = true;
            draggingObj.position = Vector3.SmoothDamp(draggingObj.position, globalMousePos, ref velocity, dampingSpeed);
        }
        else
        {
            isDragged = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If this is the object being dragged, when an object enters it, we try to create an idea
        //with a combination of this obj's name and our collision's name
        if(isDragged)ideaManager.CreateIdea(this.name, collision.transform.name);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hoverImage.localPosition = new Vector3(74.7f, -31.9f, -13f);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hoverImage.localPosition = new Vector3(10000f, 0, 0);
    }
}
