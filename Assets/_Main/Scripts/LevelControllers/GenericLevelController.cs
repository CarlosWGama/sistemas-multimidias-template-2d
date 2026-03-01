using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Classe Generica de controlador de fase
/// Basicamente controla apenas músicas da fase
/// </summary>
public class GenericLevelController : MonoBehaviour {


    [Header("Áudios")]
    public AudioResource BGM;
    public AudioResource BGS;

    void Start() {
        AudioController.Instance.PlayBGM(BGM);
        AudioController.Instance.PlayBGS(BGS);
    }

    /*
        FUNCIONALIDADES DOS BOTÕES DO MENU
    */ 
    public void BtnExit() {
        Application.Quit(); //Fecha o Jogo
    }
}
