using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public void TakeDamage(float damage)
    {
        if (health > 0) { health -= damage; }
        else { Die(); }
    }

    void Die()
    {
        Destroy(gameObject, 1f);
    }
}
