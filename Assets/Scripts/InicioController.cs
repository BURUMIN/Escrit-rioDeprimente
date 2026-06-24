using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioController : MonoBehaviour
{
    public string NomeDaFase;
    [SerializeField] private GameObject MenuConfig;
    private bool OptionCheck = false;
    

    public void Jogar()
    {
        SceneManager.LoadScene(NomeDaFase);
    }

    public void Check()
    {
        if (OptionCheck == false)
        {
            Config();
        }
        else
        {
            FechaConfig();
        }
    }

    public void Config()
    {
        MenuConfig.SetActive(true);
        OptionCheck = true;
    }

    public void FechaConfig()
    {
        MenuConfig.SetActive(false);
        OptionCheck = false;
    }

    public void Sair()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }

    public void Continuar()
    {
        
    }
}
