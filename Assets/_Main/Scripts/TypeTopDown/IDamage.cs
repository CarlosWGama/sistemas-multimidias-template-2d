using System.Collections;
using UnityEngine;

/// <summary>
/// Interface para receber dano
/// </summary>
public interface IDamage {

    public void Hit(int damage, Vector3 playerPosition);
}
