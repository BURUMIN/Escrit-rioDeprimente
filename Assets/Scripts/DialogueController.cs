using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject dialogueOBJ;
    public Image portrait;
    public Text speeachTxt;
    public Text NPCName;

    [Header("Settings")]
    public float typpingSpeed;

    public void Speech(Sprite p, string Txt, string ActorName)
    {
        dialogueOBJ.SetActive(true);
        portrait.sprite = p;
        speeachTxt.text = Txt;
        NPCName.text = ActorName;
    }
}
