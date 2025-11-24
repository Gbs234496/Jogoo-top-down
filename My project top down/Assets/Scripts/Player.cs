
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Atributos")]
    public float speed = 4f;
    public int vidaMax = 100;
    public int energiaMax = 100;

    [HideInInspector] public int vidaAtual;
    [HideInInspector] public int energiaAtual;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;

    // Movimento
    private Vector2 movement;

    [Header("Arma")]
    public Transform armaPoint; // Arraste o ponto da arma aqui no Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        vidaAtual = vidaMax;
        energiaAtual = energiaMax;
    }

    void Update()
    {
        // Movimento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool andando = movement.x != 0 || movement.y != 0;
        anim.SetBool("Andando", andando);

        // Virar personagem + arma
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (armaPoint != null)
                armaPoint.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (armaPoint != null)
                armaPoint.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // ---------------------------
    // MÉTODOS DE VIDA E ENERGIA
    // ---------------------------

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0)
            vidaAtual = 0;
    }

    public void RecuperarVida(int quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > vidaMax)
            vidaAtual = vidaMax;
    }

    public void GastarEnergia(int quantidade)
    {
        energiaAtual -= quantidade;
        if (energiaAtual < 0)
            energiaAtual = 0;
    }

    public void RecuperarEnergia(int quantidade)
    {
        energiaAtual += quantidade;
        if (energiaAtual > energiaMax)
            energiaAtual = energiaMax;
    }
}


