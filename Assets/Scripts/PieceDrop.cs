using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.CompareTag(gameObject.tag + "-certo"))
            {
                eventData.pointerDrag.GetComponent<PieceDrag>().isConnected = true;
                eventData.pointerDrag.GetComponent<PieceDrag>().shouldReturnToStartPosition = false;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                LevelData.howManyCorrect++;
                CheckWichPieceIsConnected(eventData);
                Debug.Log("Correct pieces: " + LevelData.howManyCorrect);
            }
        }
    }

    private void CheckWichPieceIsConnected(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.CompareTag("piece1-certo"))
            {
                LevelData.isPiece1Connected = true;
            }
            else if (eventData.pointerDrag.CompareTag("piece2-certo"))
            {
                LevelData.isPiece2Connected = true;
            }
            else if (eventData.pointerDrag.CompareTag("piece3-certo"))
            {
                LevelData.isPiece3Connected = true;
            }
        }
        
    }
}