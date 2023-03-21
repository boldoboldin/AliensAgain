using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent navMesh;
    private Rigidbody rb;

    [SerializeField] private GameObject player;

    [SerializeField] private float atkDist; // Distancia para atk
    [SerializeField] private float followDist; // Distancia para perceguir
    [SerializeField] private GameObject atkArea;
    public float currentFollowDist;

    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private int damage;
    [SerializeField] private int hp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.enabled) // Se navMesh estive ativo
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            bool atk = false;
            bool follow = (dist < currentFollowDist);

            if (follow && atk == false)
            {
                navMesh.SetDestination(player.transform.position); // Faz trajet�ria evitando obstaculos
                //transform.LookAt(player.transform);
            }

            if (dist < atkDist)
            {
                atk = true;
            }

            if (follow == false || atk)
            {
                navMesh.SetDestination(transform.position);
            }   

            anim.SetBool("Atk", atk); // Relaciona o valor da variavel "atk" com a anima��o "Atk"
            anim.SetBool("Walk", follow); // Relaciona o valor da variavel "follow" com a anima��o "Walk"

            }

        if (currentFollowDist >= followDist)
        {
            currentFollowDist--;
        }
    }

    public void DoAtk()
    {
        atkArea.SetActive(true);
    }

    public void UndoAtk()
    {
        atkArea.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        currentFollowDist = 300;

        if (hp <= 0)
        {
            navMesh.enabled = false;
            anim.SetBool("Atk", false);
            anim.SetBool("Walk", false);
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHP.ApplyDamage(damage);
        }
    }
}
