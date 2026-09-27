using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue {

    public string name;
    public Sprite profile; // Foto ou avatar do personagem
    [TextArea(5, 10)]
    public string text;

}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObject/TalkScript", order = 1)]
public class DialogueData : ScriptableObject {

    public List<Dialogue> talkScript;

}