using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesaController : MonoBehaviour
{
    public GameObject Reset;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Tocado");
            Reset.gameObject.SetActive(true);
        }
    }
}


