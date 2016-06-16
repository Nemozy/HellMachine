using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDetecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().MouseOverGUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().MouseOverGUI = false;
    }
}
