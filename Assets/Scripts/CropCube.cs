using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropCube : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject top;
    public GameObject bottom;
    public GameObject plant;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance.selectedProp)
        {
            if(UIManager.Instance.selectedProp == top.transform)
            {
                UIManager.Instance.HideDetail();
            }
        }
        else
        {
            if(plant)
                UIManager.Instance.ShowDetail(plant.transform);
            else
                UIManager.Instance.ShowDetail(top.transform);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance.selectedProp) return;

        top.transform.position += new Vector3(0f, 0.25f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIManager.Instance.selectedProp) return;

        top.transform.position += new Vector3(0f, -0.25f, 0f);
    }
}
