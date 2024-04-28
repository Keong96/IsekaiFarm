using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public CanvasGroup inventoryPanel;
    public CanvasGroup shopPanel;
    public CanvasGroup infoPanel;
    public int currentFarmIndex;
    public Transform selectedProp;

    public void NavigateFarm(int index)
    {
        int targetFarmIndex = currentFarmIndex + index;

        if (targetFarmIndex <= 0)
            targetFarmIndex = GameManager.Instance.userFarmList.Count - 1;
        else if (targetFarmIndex >= GameManager.Instance.userFarmList.Count)
            targetFarmIndex = 0;

        currentFarmIndex = targetFarmIndex;

        GameManager.Instance.farmParent.DOLocalMove(new Vector3(100f * currentFarmIndex, 0f, 100f * currentFarmIndex), 1f).OnComplete(()=> {
            GameManager.Instance.userFarmList[currentFarmIndex].GetComponent<Farm>().SpawnFarm();
        });
    }

    public void ShowDetail(Transform target)
    {
        infoPanel.alpha = 1f;
        infoPanel.blocksRaycasts = true;
        infoPanel.interactable = true;
    }

    public void HideDetail()
    {
        selectedProp = null;

        infoPanel.alpha = 0f;
        infoPanel.blocksRaycasts = false;
        infoPanel.interactable = false;
    }

    public void ShowInventory()
    {
        inventoryPanel.alpha = 1.0f;
        inventoryPanel.blocksRaycasts = true;
        inventoryPanel.interactable = true;
    }

    public void HideInventory()
    {
        inventoryPanel.alpha = 0.0f;
        inventoryPanel.blocksRaycasts = false;
        inventoryPanel.interactable = false;
    }

    public void ShowShop()
    {
        shopPanel.alpha = 1.0f;
        shopPanel.blocksRaycasts = true;
        shopPanel.interactable = true;
    }

    public void HideShop()
    {
        shopPanel.alpha = 0.0f;
        shopPanel.blocksRaycasts = false;
        shopPanel.interactable = false;
    }
}
