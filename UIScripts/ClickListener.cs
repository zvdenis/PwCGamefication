using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    public CharacterSkin Char;
    private void OnMouseDown()
    {
        Char.Animate();
    }
}
