using UnityEngine;

public class TrocarArma : MonoBehaviour
{
    public GameObject arma1;
    public GameObject arma2;

    private int armaIndex = 0;

    void Start()
    {
        // Garante que apenas uma arma comece ativa
        AtualizarArma();
    }

    void Update()
    {
        // Troca de arma usando qualquer ENTER
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            armaIndex = 1 - armaIndex; // Alterna entre 0 e 1
            AtualizarArma();
        }
    }

    void AtualizarArma()
    {
        if (arma1 != null && arma2 != null)
        {
            arma1.SetActive(armaIndex == 0);
            arma2.SetActive(armaIndex == 1);
        }
        else
        {
            Debug.LogError("❌ Arma1 ou Arma2 não está atribuída no Inspector!");
        }
    }
}