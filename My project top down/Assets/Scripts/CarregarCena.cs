using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    
    public string nomeDaCena;

    
    public void EntrarNaCena()
    {
        if (!string.IsNullOrEmpty(nomeDaCena))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
        else
        {
            Debug.LogWarning("Nenhum nome de cena foi definido no script CarregarCena!");
        }
    }
}

