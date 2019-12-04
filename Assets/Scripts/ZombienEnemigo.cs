using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombienEnemigo : MonoBehaviour
{
    public float visionRadio;
    public float atacarRadio;
    public float speed;
    public int maxVida;
    public int vidaActual;
    public AudioClip[] clips;
    public AudioSource Sonido;

    GameObject player;
    Vector3 inicialPosicion;     // se guarda la posicion inicial
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(("Player"));
        inicialPosicion = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vidaActual = maxVida;
        Sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 target = inicialPosicion;  //target es la posicion inicial

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadio,
            1 << LayerMask.NameToLayer("Default"));

        Vector3 forward = transform.InverseTransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if (hit.collider != null) // si el raycast encuentra al jugador se pone al target
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }
        //calcular distacia y direccion actual hasta el target
        float distacia = Vector3.Distance(target, transform.position);
        Vector3 direccion = (target - transform.position).normalized;

        // si el enemigo esta en un rango de ataque nos paramos y atacamos
        if (target != inicialPosicion && distacia < atacarRadio)
        {
            animator.SetFloat("movX", direccion.x);
            animator.SetFloat("movY", direccion.y);
        }
        else
        {
            rb.MovePosition(transform.position + direccion * speed * Time.deltaTime);
            animator.speed = 1;
            animator.SetFloat("movX", direccion.x);
            animator.SetFloat("movY", direccion.y);
            animator.SetBool("CaminarD", true);
        }

        if (target == inicialPosicion && distacia < 0.05f) //una comprobación para evitar bugs forzando la posicion inicial
        {
            transform.position = inicialPosicion;
            animator.SetBool("CaminarD", false);         
        }

        //line hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }

    public void Atacado()
    {
        //  primero se le resta 1 punto y luego se comprueba si es menor o igual a 0
        Sonido.PlayOneShot(clips[0]);
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
                pos.x - 20, // posicion de x
                Screen.height - pos.y + 16, // posicion de y (formula)
                40, //anchura 
                24 // altura
                ), vidaActual + "/" + maxVida // texto
            );
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
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
