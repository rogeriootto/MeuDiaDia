using UnityEngine;
using UnityEngine.EventSystems;


public class PieceDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Vector3 startScale;
    private Vector3 finalScale;

    [SerializeField] private float scaleMultiplier;

    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start() {
        startScale = rectTransform.localScale;
        finalScale = startScale * scaleMultiplier;
        startPosition = rectTransform.anchoredPosition;
    }

    void Update() {
        
    }

    public void OnBeginDrag(PointerEventData eventData) {
        rectTransform.localScale = finalScale;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta;
    }

     public void OnEndDrag(PointerEventData eventData){
        rectTransform.localScale = startScale;
        rectTransform.anchoredPosition = startPosition;
    }
    
}