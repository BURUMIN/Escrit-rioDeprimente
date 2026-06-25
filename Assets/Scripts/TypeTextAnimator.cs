using UnityEngine;
using TMPro;
using System.Collections;

public class TypeTextAnimator : MonoBehaviour
{
    public float TempoDaFala = 0.05f;
    public TextMeshProUGUI UITexto;

    public string TextoCompleto;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        UITexto.text = TextoCompleto;
        UITexto.maxVisibleCharacters = 0;

        for (int i = 0; i < UITexto.text.Length; i++)
        {
            UITexto.maxVisibleCharacters = i;
            yield return new WaitForSeconds(TempoDaFala);
        }
    }
}
