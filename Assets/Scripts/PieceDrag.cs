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
    public GameObject rightPieceVFX;
     public AudioClip correctSFX;

    [SerializeField] private float scaleMultiplier;
    [SerializeField] private GameObject afterImage;
    private CardEffect cardEffect;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        cardEffect = afterImage.GetComponent<CardEffect>();
    }

    void Start() {
        startScale = rectTransform.localScale;
        finalScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return; 
        Debug.Log("OnBeginDrag called on " + gameObject.name);
        rectTransform.localScale = finalScale;
        cardEffect.FinalScale();
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
            cardEffect.NormalScale();
            LevelData.howManyWrong++;

        }
        else if (GameData.Instance.selectedPlayer.fancyGraphicsOn)
        {
            Instantiate(rightPieceVFX, new Vector3(transform.position.x, transform.position.y, 50), Quaternion.identity);
            AudioSource.PlayClipAtPoint(correctSFX, Camera.main.transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(correctSFX, Camera.main.transform.position);
        }

        LevelData.howManyTries++;
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
    }
    
}