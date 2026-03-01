using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script genérico para qualquer botão que o unico papel é carregar outra tela
/// </summary>
public class ButtonGenericNavigationUI : MonoBehaviour {
    
    /// <summary>
    /// Ação do botão
    /// </summary>
    /// <param name="scene">Nome da Scene</param>
    public void BtnNavigate(string scene) {
        SceneManager.LoadScene(scene);        
    }
}
