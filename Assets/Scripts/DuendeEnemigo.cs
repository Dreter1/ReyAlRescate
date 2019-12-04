using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuendeEnemigo : MonoBehaviour
{
    public float visionRadio;
    public float atacarRadio;
    public float speed;
    public int maxVida;
    public int vidaActual;
    public bool atacando = false;
    GameObject player;
    // se guarda la posicion inicial
    Vector3 inicialPosicion;
    Animator animator;
    Rigidbody2D rb;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(("Player"));
        inicialPosicion = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vidaActual = maxVida;
    }

    void Update()
    {
        //target es la posicion inicial
        Vector3 target = inicialPosicion;

        RaycastHit2D hit = Physics2D.Raycast(

            transform.position,
            player.transform.position -transform.position,
            visionRadio,
            1 << LayerMask.NameToLayer("Default")

            );

        Vector3 forward = transform.InverseTransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // si el raycast encuentra al jugador se pone al target

        if (hit.collider!=null)
        {
            if (hit.collider.tag=="Player")
            {
                target = player.transform.position;
            }
        }
        //calcular distacia y direccion actual hasta el target
        float distacia = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        // si el enemigo esta en un rango de ataque nos paramos y atacamos

        if (target!= inicialPosicion && distacia < atacarRadio)
        {
            animator.SetFloat("movX", dir.x);
            animator.SetFloat("movY", dir.y);
        }
        else
        {    
            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
            animator.speed = 1;
            animator.SetFloat("movX", dir.x);
            animator.SetFloat("movY", dir.y);
            animator.SetBool("CaminarD", true);
        }

        if (target != inicialPosicion && atacarRadio > 0.05f  )
        {
            animator.SetFloat("movX", dir.x);
            animator.SetFloat("movY", dir.y);
            animator.SetBool("AtacarD", true);
            atacando = true;
        }

        //una comprobación para evitar bugs forzando la posicion inicial

        if (target == inicialPosicion && distacia <0.05f)
        {
            transform.position = inicialPosicion;
            
            animator.SetBool("CaminarD", false);
            animator.SetBool("AtacarD", false);
            atacando = false;
        }

        //line hasta el target
        Debug.DrawLine(transform.position, target,Color.green);
    }

    public void Atacado()
    {
        //  primero se le resta 1 punto y luego se comprueba si es menor o igual a 0
        if (--vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        //Guarda la posicion del enemigo
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        //Dibujo del Cuadro de texto
        GUI.Box(

            new Rect(
                pos.x -20 , // posicion de x
                Screen.height -pos.y +16, // posicion de y (formula)
                40, //anchura 
                24 // altura
                ) , vidaActual + "/" + maxVida // texto
            ) ; 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && atacando == true)
        {           
            // ejecuta el metodo ataca(prueba si exste y lo ejecuta)
            coll.gameObject.SendMessage("Atacado");
        }       
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.name == "AtaqueEspada")
        {
            Debug.Log("Duende Atacado Espada");
            Atacado();
        }
    }
}
