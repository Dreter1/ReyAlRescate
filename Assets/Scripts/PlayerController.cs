using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform tr;
    public float speed = 4f;
    public bool atacando = false;
    public bool arco = false;
    public Image barra;
    public float maxVida = 5;
    public float vidaActual;
    CircleCollider2D espadaColider;

    public Vector2 movimiento;

    public int numeroFlechas = 5;
    public Text Texto;
    private int cantidadflecha;
    private int puntos;

    public GameObject FlechaA;
    public GameObject FlechaAb;
    public GameObject FlechaIz;
    public GameObject FlechaD;
    public GameObject ObjetoVacioDerecha;
    public GameObject ObjetoVacioIzquierda;
    public GameObject ObjetoVacioArriba;
    public GameObject ObjetoVacioAbajo;
    public GameObject Reset;
    public GameObject Ganar;
    public GameObject msj;
    public GameObject aviso1;
    public GameObject avisoLLave;
    public GameObject Llaves;

    public AudioClip[] clips;
    public AudioSource Sonido;

    void Start()
    {
        Sonido = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        espadaColider = transform.GetChild(8).GetComponent<CircleCollider2D>();
        espadaColider.enabled = false;
        vidaActual = maxVida;
        barra.fillAmount = vidaActual / maxVida;
        Reset.gameObject.SetActive(false);
        Ganar.gameObject.SetActive(false);
        msj.gameObject.SetActive(true);
        aviso1.gameObject.SetActive(false);
        avisoLLave.gameObject.SetActive(false);
    }

    void Update()
    {
        MostrarMensajes();
        IniciarMovimientos();
        AtacarConEspada();
       
         AtacarConArco();
    }
    private void MostrarMensajes()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Mensaje se muestra y pasa con espacio
        {
            msj.gameObject.SetActive(false);
            aviso1.gameObject.SetActive(false);
            avisoLLave.gameObject.SetActive(false);
        }
    }

    private void IniciarMovimientos()
    {
        //GetAxisRaw devuelve el valor del eje 
        movimiento = new Vector2(
            Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")
            );
        animator.SetFloat("movX", movimiento.x);
        animator.SetFloat("movY", movimiento.y); 
    }

    private void AtacarConEspada()
    {
        AnimatorStateInfo estadoEspada = animator.GetCurrentAnimatorStateInfo(0);
        bool espadaso = estadoEspada.IsName("Ataque");

        // Atacar con espada presionando la tecla X y/o flecha para arriba,abajo,izquierda o derecha
        if (Input.GetKeyDown(KeyCode.X) && !espadaso  && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            animator.SetBool("Atacar", true);
            atacando = true;
            animator.SetTrigger("Atacar");
            Sonido.PlayOneShot(clips[0]);
        }
        else
        {
            animator.SetBool("Atacar", false);
            atacando = false;
        }

        if (movimiento != Vector2.zero) espadaColider.offset = new Vector2(movimiento.x / 4, movimiento.y / 4);

        if (espadaso)
        {
            float playbackTime = estadoEspada.normalizedTime;
            if (playbackTime > 0.62 && playbackTime < 1.24) espadaColider.enabled = true;
            else espadaColider.enabled = false;
        }

    }

    private void AtacarConArco()
    {
        //usar Arco
        if (Input.GetKeyDown(KeyCode.Z) && numeroFlechas > 0 && Input.GetKey(KeyCode.UpArrow))
        {
            SetearArco();
            FlechaParaArriba();
            DisminuirFlechas();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && numeroFlechas > 0 && Input.GetKey(KeyCode.DownArrow))
        {
            SetearArco();
            FlechaParaAbajo();
            DisminuirFlechas();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && numeroFlechas > 0 && Input.GetKey(KeyCode.RightArrow))
        {
            SetearArco();
            FlechaParaLaDerecha();
            DisminuirFlechas();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && numeroFlechas > 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            SetearArco();
            FlechaParaLaIzquierda();
            DisminuirFlechas();
        }
        else
        {
            animator.SetBool("Arco", false);
            arco = false;
        }
    }

    private void SetearArco()
    {
        // Se activa el arco
        animator.SetBool("Arco", true);
        arco = true;
        animator.SetTrigger("Arco");
    }

    private void DisminuirFlechas()
    {
        // Disminuye flechas
        numeroFlechas--;
        cantidadflecha = int.Parse(Texto.text) - 1;
        Texto.text = cantidadflecha.ToString();
        Sonido.PlayOneShot(clips[1]);
    }

    private void FlechaParaArriba()
    {
        var flechaA = FlechaA.GetComponent<SpriteRenderer>();
        Instantiate(FlechaA, ObjetoVacioArriba.transform.position, Quaternion.identity);
    }

    private void FlechaParaAbajo()
    {
        var flechaB = FlechaAb.GetComponent<SpriteRenderer>();
        Instantiate(FlechaAb, ObjetoVacioAbajo.transform.position, Quaternion.identity);
    }

    private void FlechaParaLaDerecha()
    {
        var flechaD = FlechaD.GetComponent<SpriteRenderer>();
        Instantiate(FlechaD, ObjetoVacioDerecha.transform.position, Quaternion.identity);
    }

    private void FlechaParaLaIzquierda()
    {
        var flechaI = FlechaIz.GetComponent<SpriteRenderer>();
        Instantiate(FlechaIz, ObjetoVacioIzquierda.transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemigo" && atacando == true)
        {
            Debug.Log("Tocado");
            coll.gameObject.SendMessage("Atacado Enemigo");
        }

        if (coll.gameObject.name == "Cartel")
        {
            aviso1.gameObject.SetActive(true);
        }

        if (coll.gameObject.name == "CartelLLave")
        {
            avisoLLave.gameObject.SetActive(true);
        }
    }

    public void Atacado()
    {
        //  primero se le resta 1 
        --vidaActual;
        barra.fillAmount = vidaActual / maxVida;
        Sonido.PlayOneShot(clips[4]);
        //  se comprueba si es menor o igual a 0
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
            Reset.gameObject.SetActive(true);
            Sonido.PlayOneShot(clips[3]);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Corazon")
        {
            Debug.Log("corazon");
            ++vidaActual;
            if (vidaActual>5)
            {
                vidaActual = 5;
            }
            barra.fillAmount = vidaActual / maxVida;
            Destroy(col.gameObject);
            Sonido.PlayOneShot(clips[2]);
        }

        if (col.gameObject.tag == "Flecha")
        {
            Debug.Log("Recargaste tus flecha");
            numeroFlechas += 5;
            cantidadflecha = int.Parse(Texto.text) + 5;
            Texto.text = cantidadflecha.ToString();
            Destroy(col.gameObject);
        }
    }
}
