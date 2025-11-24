/*
using UnityEngine;

public class Inimigo : Personagem
{
    [SerializeField] private int dano = 1;
    
    public float raioDeVisao = 1;
    public CircleCollider2D _visaoCollider2D;

    [SerializeField] private Transform posicaoDoPlayer;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private AudioSource audioSource;
    
    
    private bool andando = false;
    
    public void setDano(int dano)
    {
        this.dano = dano;
    }
    public int getDano()
    {
        return this.dano;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        
        if (posicaoDoPlayer == null)
        {
            posicaoDoPlayer =  GameObject.Find("Player").transform;
        }
        
        raioDeVisao = _visaoCollider2D.radius;
    }

    void Update()
    {
        andando = false;

        if (getVida() > 0)
        {
            if (posicaoDoPlayer.position.x - transform.position.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (posicaoDoPlayer.position.x - transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }


            if (posicaoDoPlayer != null &&
                Vector3.Distance(posicaoDoPlayer.position, transform.position) <= raioDeVisao)
            {
                Debug.Log("Posição do Pluer" + posicaoDoPlayer.position);

                transform.position = Vector3.MoveTowards(transform.position,
                    posicaoDoPlayer.transform.position,
                    getVelocidade() * Time.deltaTime);

                andando = true;
            }

        }

        if (getVida() <= 0)
        {
            animator.SetTrigger("Morte");
        }
        
        animator.SetBool("Andando",andando);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causa dano ao Player
            int novaVida = collision.gameObject.GetComponent<Personagem>().getVida() - getDano();
            collision.gameObject.GetComponent<Personagem>().setVida(novaVida);

            //collision.gameObject.GetComponent<Personagem>().recebeDano(getDano());
            
            setVida(0);
        }
    }

    public void playAudio()
    {
        audioSource.Play();
    }

    public void desative()
    {
        //desativa quando bate no player
         gameObject.SetActive(false);
    }
}
*/

using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Transform player;    // arraste o Player aqui
    public float velocidade = 3f;
    public float distanciaParaParar = 0.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Distância entre inimigo e player
        float distancia = Vector2.Distance(transform.position, player.position);

        // Se estiver longe, segue
        if (distancia > distanciaParaParar)
        {
            Vector2 direcao = (player.position - transform.position).normalized;
            rb.linearVelocity = direcao * velocidade;
        }
        else
        {
            // Se chegou perto, PARA
            rb.linearVelocity = Vector2.zero;
        }
    }

    // Para NÃO empurrar o player ao colidir
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero; // trava movimento
        }
    }
}