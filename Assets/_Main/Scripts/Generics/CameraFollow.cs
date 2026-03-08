using UnityEngine;

/// <summary>
/// Script Responsável por fazer a camera seguir o Player
/// </summary>
public class CameraFollow : MonoBehaviour {
    [Header("Seguir alvo")]
    public GameObject target;
    public float speed = 2f;
    // ---------------------------------------
    void Update() {
        if (target) {
            var destination = target.transform.position;
            destination.z = transform.position.z; //Não altera a distância da câmera
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }
    }



}
