using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent navMesh;

    private GameObject player;

    [SerializeField] private float atkDist; // Distancia para atk
    [SerializeField] private float followDist; // Distancia para perceguir
    public float currentFollowDist = 15; 

    [SerializeField] private int damage;
    [SerializeField] private int hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.enabled) // Se navMesh estive ativo
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            bool atak = false;
            bool follow = (dist < currentFollowDist);

            if (follow)
            {
                if(dist < atkDist)
                {
                    atak = true;
                    transform.LookAt(player.transform);
                }

                navMesh.SetDestination(player.transform.position); // Faz trajetória evitando obstaculos
            }

            if (follow == false || atak == true)
            {
                navMesh.SetDestination(transform.position);
            }

            //anim.SetBool("Atak", atak);
            //anim.SetBool("Walk", follow);
        }

        if (currentFollowDist >= followDist)
        {
            currentFollowDist--;
        }
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        currentFollowDist = 300;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
