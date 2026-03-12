using UnityEngine;

public class PlayerElfTopDown : PlayerTopDown {

    public GameObject arrow;
    
    protected override bool Attack() {
        var attacked = base.Attack();
        if (attacked) {
            Arrow arrowObj = Instantiate(arrow, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity).GetComponent<Arrow>();
            arrowObj.DefineDirection(transform.localScale.x == 1); //Direita = 1 | Esquerda = -1
        }
        return attacked;
    }
}
