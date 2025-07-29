using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using TMPro;

public class CardEffect : MonoBehaviour
{

    private RectTransform rectTransform;
    [SerializeField] private GameObject mainLogic;
    private HintLogic hint;

    private PieceDrag pieceDrag;
    private Vector3 movementDelta;
    private Vector3 rotationDelta;

    public Vector3 startScale;

    [SerializeField] private float rotationAmount = 20;
    // [SerializeField] private float movementAmount = 10;
    [SerializeField] private float movementSpeed = 0.7f;
    [SerializeField] private float rotationSpeed = 2;

    Vector3 movementRotDelta;
    Vector3 movementRotation;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        pieceDrag = mainLogic.GetComponent<PieceDrag>();
        hint = GetComponent<HintLogic>();
        startScale = rectTransform.localScale;
    }

    public void Update()
    {
        if (mainLogic.GetComponent<PieceDrag>().isConnected)
        {
            FinalScale();
        }

        if (hint == null)
        {
            hint = GetComponent<HintLogic>();
        }

        if (GameData.Instance.selectedPlayer.fancyGraphicsOn)
        {
            SmothMovement();
            SmothRotation();
        }
        else
        {
            NormalMovement();
        }
        
    }

    private void SmothMovement()
    {
        movementDelta = Vector3.Lerp(rectTransform.anchoredPosition, mainLogic.GetComponent<RectTransform>().anchoredPosition, movementSpeed);
        rectTransform.anchoredPosition = movementDelta;
    }

    private void SmothRotation()
    {
        Vector3 directionVector = rectTransform.anchoredPosition - mainLogic.GetComponent<RectTransform>().anchoredPosition;

        movementRotDelta = Vector3.Lerp(movementRotDelta, directionVector, 25 * Time.deltaTime);
        movementRotation = (pieceDrag.isDragging ? movementRotDelta : directionVector) * rotationAmount;
        rotationDelta = Vector3.Lerp(rotationDelta, movementRotation, rotationSpeed * Time.deltaTime);
        rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, Mathf.Clamp(rotationDelta.x, -20, 20));
    }

    private void NormalMovement()
    {
        rectTransform.anchoredPosition = mainLogic.GetComponent<RectTransform>().anchoredPosition;
    }

    private void ScaleLogic()
    {
        if (pieceDrag.isConnected || pieceDrag.isDragging)
        {
            rectTransform.localScale = pieceDrag.finalScale;
        }
        else if (!pieceDrag.isConnected || !hint.hint)
        {
            rectTransform.localScale = startScale;
        }

    }

    public void NormalScale()
    {
        rectTransform.localScale = startScale;
    }

    public void FinalScale()
    {
        rectTransform.localScale = pieceDrag.finalScale;
    }
    
}