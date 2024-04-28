using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Transform> inventoryItem;
    public Button actionButton;
    public int selectedIndex;


    private void Start()
    {
        selectedIndex = -1;

        actionButton.interactable = false;

        foreach (Transform t in transform.GetChild(0))
        {
            inventoryItem.Add(t);
        }
    }

    public void SelectItem(int index)
    {
        foreach (Transform t in inventoryItem)
        {
            t.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }

        inventoryItem[index].GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 0.5f);
        selectedIndex = index;

        actionButton.interactable = true;
    }
}
