using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{
    public float tempoParaMudarDeCena = 2f;
    public string cenaDestino;

    void Update()
    {
        tempoParaMudarDeCena -= Time.deltaTime;
        if (tempoParaMudarDeCena <= 0f)
        {
            SceneManager.LoadScene(cenaDestino);
        }
    }
}
