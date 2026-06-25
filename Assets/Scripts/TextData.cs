using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable] 
 public struct Textos
    {
        public string Nome;
        [TextArea(5,10)]
        public string Fala;
    internal string text;
}

[CreateAssetMenu(fileName = "TextData", menuName = "TextData", order = 1)]
public class TextData : ScriptableObject
{

    public List<Textos> Roteiros;
}
