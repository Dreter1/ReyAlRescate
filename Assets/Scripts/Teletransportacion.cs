using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransportacion : MonoBehaviour
{
    public GameObject target;

    void Awake() //desaparecer puntos de Teletransportación
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //GetChild = tranforma al hijo
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; ;
    }
   void OnTriggerEnter2D(Collider2D col) // cambiar punto de teletransportacion
    {
        if (col.tag =="Player")
        {
            col.transform.position = target.transform.GetChild(0).transform.position;
        }
    }
}
