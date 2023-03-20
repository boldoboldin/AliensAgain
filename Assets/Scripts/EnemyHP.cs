using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int hp = 100;

    public void ApplyDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
