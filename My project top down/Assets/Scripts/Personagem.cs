using UnityEngine;

public class Personagem : MonoBehaviour
{
   
    public float velocidade = 5f;
    public int vida = 10;
    public int energia = 100;

    
    void Update()
    {
        Mover();
    }

    void Mover()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(horizontal, 0, vertical);
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    // Método para aplicar dano
    public void ReceberDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            vida = 0;
            Morrer();
        }
    }

    // Método para gastar energia
    public void GastarEnergia(int quantidade)
    {
        energia -= quantidade;
        if (energia < 0)
        {
            energia = 0;
        }
    }

    void Morrer()
    {
        Debug.Log("O personagem morreu!");
        // Aqui você pode adicionar lógica para reiniciar o jogo ou destruir o objeto.
    }
}