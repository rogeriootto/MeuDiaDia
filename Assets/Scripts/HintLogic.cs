using UnityEngine;

public class HintLogic : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private GameObject mainLogic;
    public bool hint = false;
    private bool breathingIn = true;

    private Vector3 breathIn = new Vector3(1f, 1f, 1f);
    private Vector3 breathOut = new Vector3(0.8f, 0.8f, 0.8f);
    private float breathTime = 0f;
    private readonly float expandDuration = 0.3f;
    
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        breathTime = 0f;
        breathingIn = true;
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
        Debug.Log("Hint logic active");
        Debug.Log("breathingIn: " + breathingIn);
        Vector3 targetScale = breathingIn ? breathIn : breathOut;
        Vector3 startScale = breathingIn ? breathOut : breathIn;

        breathTime += Time.deltaTime;
    
        float lerpfactor = breathTime / expandDuration;

        rectTransform.localScale = Vector3.Lerp(startScale, targetScale, lerpfactor);
        
        if (lerpfactor >= 1f)
        {
            breathingIn = !breathingIn;
            breathTime = 0f;
        }
    }
}
