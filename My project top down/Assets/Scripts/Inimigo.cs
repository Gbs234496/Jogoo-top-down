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

public class InimigoSeguir : MonoBehaviour
{
    public Transform player;      // arraste o Player no inspetor
    public float velocidade = 3f; // velocidade do inimigo
    public float distanciaMinima = 0.5f; // distância para não colar no Player

    void Update()
    {
        if (!player)
            return;

        // Distância entre o inimigo e o player
        var distancia = Vector3.Distance(transform.position, player.position);

        // Se o player estiver se movendo, o inimigo segue.
        // Se o player parar, ele apenas para porque não precisa ajustar direção.
        if (distancia > distanciaMinima)
        {
            // Move em direção ao player
            Vector3 direcao = (player.position - transform.position).normalized;
            transform.position += direcao * (velocidade * Time.deltaTime);
        }
    }

    // Garante que não cause dano (impede qualquer trigger de dano)
    void OnCollisionEnter(Collision collision)
    {
        // Não faz nada — nenhum dano é aplicado
    }

    void OnTriggerEnter(Collider other)
    {
        // Também não faz nada — nenhuma lógica de dano
    }
}