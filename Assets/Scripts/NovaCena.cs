using UnityEngine;
using UnityEngine.SceneManagement;

public class NovaCena : MonoBehaviour
{
    public string NomeDaFase;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                carregarCena();
        }
    }

    private void carregarCena()
    {
        SceneManager.LoadScene(NomeDaFase);
    }
}
