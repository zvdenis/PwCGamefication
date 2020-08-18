using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    // Start is called before the first frame update
    
    [ExecuteAlways]
    private void Update()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

     
}
