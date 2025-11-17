/*
using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {

    }



    void Update()
    {

        var player = Vector3.zero;


        if (Input.GetKey(KeyCode.UpArrow))
        {
            player += transform.forward;
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            player += -transform.forward;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            player += transform.right;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player += -transform.right;
        }


        player.Normalize();
    }

}


using UnityEngine;

public class Player : Personagem
{
    [Header("Configurações de Movimento")]
    public float speed = 5f; // Velocidade do personagem
    private bool andando = false;

    public global::System.Boolean Andando { get => andando; set => andando = value; }

    void Update()
    {
        // Detecta as setas do teclado
        float moveX = 0f;
        float moveY = 0f;
        Andando = false;
        


        if (Input.GetKey(KeyCode.RightArrow))
        { 
            // seta → direita
            moveX = 1f;
            Andando = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) 
        { 
            // seta ← esquerda
            moveX = -1f;
            Andando = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
           // seta ↑ cima
            moveY = 1f;
            Andando = true;  
        }  

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // seta ↓ baixo
            moveY = -1f;
            Andando = true;
        }   

        // Cria o vetor de movimento
        Vector3 move = new Vector3(moveX, moveY, 0f).normalized;

        // Aplica o movimento no personagem
        transform.position += move * (speed * Time.deltaTime);

        // Mostra a posição atual no console (opcional)
        Vector3 pos = transform.position;
        Debug.Log($"Posição atual → X: {pos.x:F2}, Y: {pos.y:F2}");
    }
}
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;

    private Rigidbody2D rb;
    private Animator anim;

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Entradas
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Ativa animação SOMENTE quando houver movimento
        bool andando = movement.x != 0 || movement.y != 0;
        anim.SetBool("Andando", andando); // <-- aqui está o nome correto

        // Virar personagem
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}