using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFixer : MonoBehaviour
{
    public int order;

    private void Move()
    {
        gameObject.transform.SetSiblingIndex(order);
    }

    private void Start()
    {
        Move();
    }
}