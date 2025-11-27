using UnityEngine;

public class TrocarArma : MonoBehaviour
{
    // Coloque suas armas aqui no Inspector
    public GameObject arma1;
    public GameObject arma2;

    private GameObject armaAtual;

    void Start()
    {
        // Começa com arma1 ativa e arma2 desativada
        armaAtual = arma1;
        arma1.SetActive(true);
        arma2.SetActive(false);
    }

    void Update()
    {
        // Troca de arma ao pressionar Enter
        if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            Trocar();
        }
    }

    void Trocar()
    {
        if (armaAtual == arma1)
        {
            arma1.SetActive(false);
            arma2.SetActive(true);
            armaAtual = arma2;
        }
        else
        {
            arma2.SetActive(false);
            arma1.SetActive(true);
            armaAtual = arma1;
        }
    }
}