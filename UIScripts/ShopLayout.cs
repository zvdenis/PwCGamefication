using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UnityEngine;

public class ShopLayout : MonoBehaviour
{
    public GameObject panel;
    public GameObject ShopElement;
    public GameObject ElementParent;


    private List<GameObject> elemntObjects = new List<GameObject>();
    private List<ShopElement> elements = new List<ShopElement>();

    public void UpdateShop()
    {
        foreach (var obj in elemntObjects)
        {
            Destroy(obj);
        }

        elemntObjects.Clear();
        elements.Clear();

        foreach (var item in ItemList.Items)
        {
            GameObject tmp = Instantiate(ShopElement, ElementParent.transform);
            elemntObjects.Add(tmp);
            ShopElement element = tmp.GetComponent<ShopElement>();
            elements.Add(element);
            element.SetItem(item.Id);
        }
    }

    private void OnEnable()
    {
        UpdateShop();
    }

    private void Start()
    {
        //UpdateShop();
    }
}