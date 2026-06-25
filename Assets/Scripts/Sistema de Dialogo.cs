// using Unity.VisualScripting;
// using UnityEngine;

// public enum ESTADO
// {
//     DESATIVADO,
//     ESCREVENDO,
//     ESPERANDO  
// }
// public class SistemadeDialogo : MonoBehaviour
// {

//     public TextData TextData;
//     int TextoAtual = 0;
//     bool FimDePapo = false;

//     TypeTextAnimator TypeText;
//     ESTADO Estado;

//     void Awake()
//     {
//         TypeText = FindAnyObjectByType<TypeTextAnimator>();
//     }
//     void Start()
//     {
//             Estado = ESTADO.DESATIVADO;
//     }

//     void Update()
//     {
//         if (Estado == ESTADO.DESATIVADO) return;

//         switch (Estado)
//         {
//             case ESTADO.ESPERANDO:
//                 Esperando();
//                 break;

//             case ESTADO.ESCREVENDO:
//                 Escrevendo();
//                 break; 
//         }
//     }

//     void Proximo()
//     {
//         TypeText.TextoCompleto = TextData.Roteiros[TextoAtual].text;
//         if (TextoAtual == TextData.Roteiros.Count) FimDePapo = true;

//         TypeText.StartTyping();
//     }

//     void Esperando()
//     {
        
//     }

//     void Escrevendo()
//     {
        
//     }
// }
