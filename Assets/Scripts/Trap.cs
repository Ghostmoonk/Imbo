using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Character>().Die();
        }
    }
}
