using UnityEngine;
using UnityEngine.EventSystems;


public class PieceDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    public Vector3 startScale;
    public Vector3 finalScale;
    public bool isConnected = false;
    public bool shouldReturnToStartPosition = true;

    public bool isDragging = false;

    [SerializeField] private float scaleMultiplier;

    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start() {
        startScale = rectTransform.localScale;
        finalScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
        startPosition = rectTransform.anchoredPosition;
    }

    void Update() {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return; 
        Debug.Log("OnBeginDrag called on " + gameObject.name);
        rectTransform.localScale = finalScale;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (isConnected) return;
        isDragging = true;

        rectTransform.anchoredPosition += eventData.delta;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (shouldReturnToStartPosition && !isConnected)
        {

            rectTransform.anchoredPosition = startPosition;
            rectTransform.localScale = startScale;
            LevelData.howManyWrong++;

        }
        LevelData.howManyTries++;
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
    }
    
}