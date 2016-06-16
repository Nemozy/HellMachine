using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public string parameter;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            object argument = true;
            GameObject.Find("Player").transform.Find("Ship").SendMessage("Go" + parameter, argument);
            this.transform.Find("AutoOn").gameObject.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            object argument = false;
            GameObject.Find("Player").transform.Find("Ship").SendMessage("Go" + parameter, argument);
            this.transform.Find("AutoOn").gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this.transform.Find("AutoOn").gameObject.SetActive(GameObject.Find("Player").transform.Find("Ship").GetComponent<HeroMovingController>().InverseParameter(parameter));
        }
    }
}
