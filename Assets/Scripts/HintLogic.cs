using UnityEngine;

public class HintLogic : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private GameObject mainLogic;
    private PieceDrag pieceDrag;
    public bool hint = false;
    private bool breathingIn = true;

    private Vector3 breathIn = new Vector3(1f, 1f, 1f);
    private Vector3 breathOut = new Vector3(0.8f, 0.8f, 0.8f);
    private static float breathTime = 0f;
    private readonly float expandDuration = 0.3f;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        pieceDrag = mainLogic.GetComponent<PieceDrag>();
    }

    void Update()
    {
        if (hint)
        {
            Hint();
        }

    }

    void Hint()
    {
        Vector3 targetScale = breathingIn ? breathIn : breathOut;
        Vector3 startScale = breathingIn ? breathOut : breathIn;

        breathTime += Time.deltaTime;
    
        float lerpfactor = breathTime / expandDuration;

        rectTransform.localScale = Vector3.Lerp(startScale, targetScale, lerpfactor);
        if (lerpfactor >= 1f) {
            breathingIn = !breathingIn;
            breathTime = 0f;
        }
    }
}
