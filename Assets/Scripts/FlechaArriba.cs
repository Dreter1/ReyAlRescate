using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlechaArriba : MonoBehaviour
{
    public float velocidad = 30;
   
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    void Start()
    {
        Debug.Log("Script Flecha Arriba");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!sr.flipY)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocidad);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Enemigo")
        {
            Debug.Log("Mataste un Enemigo");
            col.gameObject.SendMessage("Atacado");
            Destroy(this.gameObject);
        }
    }
}
