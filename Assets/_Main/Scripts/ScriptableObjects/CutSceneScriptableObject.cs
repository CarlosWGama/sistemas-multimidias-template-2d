using UnityEngine;

[CreateAssetMenu(fileName = "CutSceneScriptableObject", menuName = "Scriptable Objects/CutScene")]
public class CutSceneScriptableObject : ScriptableObject {
    
    public Sprite image;
    [Multiline]
    public string Text;
}
