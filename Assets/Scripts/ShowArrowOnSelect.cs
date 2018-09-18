using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowArrowOnSelect : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Button>().Select();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.Find("Arrow").gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.Find("Arrow").gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("Arrow").gameObject.SetActive(true);
        GetComponent<Button>().Select();
    }   
}