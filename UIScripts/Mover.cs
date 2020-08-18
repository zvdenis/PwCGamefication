using System.Collections;
using UnityEngine;

namespace UI_scripts
{
    public class Mover : MonoBehaviour
    {
        public int moveRange;
        public GameObject moveObject;

        private void Start()
        {
            StartCoroutine(moveDelay());
        }

        IEnumerator moveDelay()
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 pos = moveObject.transform.position;
            pos.z += moveRange;
            moveObject.transform.position = pos;   
        }
    
    }
}