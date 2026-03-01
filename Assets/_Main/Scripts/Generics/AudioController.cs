using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    private AudioSource BGM;
    private AudioSource BGS;
    private AudioSource SE;

    private static AudioController instance;
    public static AudioController Instance {
        get { return instance; }
    }

    void Awake() {
        //Garante que não terá outro objetos de audio além do atual
        if (GameObject.FindObjectsByType<AudioController>(FindObjectsSortMode.None).Length > 1) {
            Destroy(this);
            return ;
        } 
        //Faz com que esse objeto continue existindo na próxima scene, ou seja, não precisa recria-lo e nem perde as configurações anteriores
        DontDestroyOnLoad(this);
        //Aplica padrão Singleton
        instance = this;

        //Busca os audios associado aos filhos
        BGM = transform.GetChild(0).GetComponent<AudioSource>();
        BGS = transform.GetChild(1).GetComponent<AudioSource>();
        SE = transform.GetChild(2).GetComponent<AudioSource>();
    }

    /// <summary> Reproduz a música de fundo </summary>
    /// <param name="audio">Áudio selecionado</param>
    public void PlayBGM(AudioResource audio) {
        if (audio != BGM.resource) { //Só troca se o audio for diferente
            BGM.resource = audio;
            BGM.Play();
        }
    }

    /// <summary> Reproduz o som de fundo continuo </summary>
    /// <param name="audio">Áudio selecionado</param>
    public void PlayBGS(AudioResource audio) {
        if (audio != BGS.resource) {
            BGS.resource = audio;
            BGS.Play();
        }
    }

    /// <summary> Reproduz um som executado uma unica vez </summary>
    /// <param name="audio">Áudio selecionado</param>
    public void PlaySE(AudioClip audio) {
        SE.PlayOneShot(audio);
    }
}

