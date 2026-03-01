using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Seleção de Personagem")]
public class SelectPlayerScriptableObject : ScriptableObject {
    public Sprite ConceptArt;
    public string title;
    [TextArea]
    public string bio;    
    public AudioClip voice;
}
