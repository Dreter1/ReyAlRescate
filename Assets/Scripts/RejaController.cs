using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejaController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.name == "LlaveFinal")
        {
            Debug.Log("TOCO REJA");
            Destroy(this.gameObject);
        }
    }
}
