using UnityEngine;
using UnityEngine.UI;

public class PlayerTopDown : CharacterTopDown {
    
 
    public Slider hpBar;

    private PlayerInputs inputs;
    private Canvas canvas;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        animator = GetComponent<Animator>();
        
        inputs = new PlayerInputs();
        canvas = GetComponentInChildren<Canvas>();
        hpBar = GetComponentInChildren<Slider>();
        hpBar.value = hpBar.maxValue = HP;
    }

    void OnEnable() => inputs.Enable();
    void OnDisable() => inputs.Disable();

    /// <summary>
    /// Função usada para elementos que envolve fisica, mantendo sempre a constancia da execução
    /// </summary>
    void FixedUpdate() {
        Move();        
    }

    void Update() {
        Attack();
    }


    //Ataca
    protected void Attack() {
        if (inputs.Player.Attack.WasPressedThisFrame()) {
            animator.SetTrigger("Attack");    
            StartCoroutine(CanMove(0.7f));
        }
    }

    //Movimenta o personagem
    private void Move() {
        if (!canMove) return;

        //Movimento
        var direction = inputs.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = direction * speed;
        //animação
        animator.SetBool("Walking", direction != Vector2.zero);
        //Giro
        if (direction.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
            if (canvas.transform.localScale.x < 0) //Evita espelha a barra
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
        else if (direction.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
            if (canvas.transform.localScale.x > 0) //Evita espelha a barra
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
    }

    public override bool Hit(int damage, Vector3 enemyPosition) {
        var hitted = base.Hit(damage, enemyPosition);
        hpBar.value = HP;       

        if (hitted && HP <= 0) {
            //Game Over
            TopDownLevelController.Instance.GameOver();
        }
        return hitted;
    }

    
}
