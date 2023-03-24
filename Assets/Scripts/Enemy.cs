using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent navMesh;
    private Rigidbody rb;

    private bool awake = false;

    [SerializeField] private GameObject player;

    [SerializeField] private float atkDist; // Distancia para atk
    [SerializeField] private float followDist; // Distancia para perceguir
    [SerializeField] private GameObject atkArea;
    public float currentFollowDist;
    private float dist;

    private AudioSource sfx;
    [SerializeField] private AudioClip atkSFX;
    [SerializeField] private AudioClip followSFX;

    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private int damage;
    [SerializeField] private int hp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.enabled) // Se navMesh estive ativo
        {
            bool atk = false;
            bool follow = (dist < currentFollowDist);
            dist = Vector3.Distance(player.transform.position, transform.position);


            if (follow && atk == false)
            {
                navMesh.SetDestination(player.transform.position); // Faz trajetória evitando obstaculos
            }

            if (follow == false)
            {
                navMesh.SetDestination(transform.position);
            }

            if (dist < atkDist)
            {
                atk = true;
                currentFollowDist = 0;
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }

            if (awake)
            {
                FollowAgain();
                sfx.PlayOneShot(followSFX);
            }

            anim.SetBool("Atk", atk); // Relaciona o valor da variavel "atk" com a animação "Atk"
            anim.SetBool("Walk", follow); // Relaciona o valor da variavel "follow" com a animação "Walk"

        }

        if (currentFollowDist >= followDist)
        {
            currentFollowDist = currentFollowDist - Time.deltaTime * 7;
        }
    }

    public void DoAtk()
    {
        atkArea.SetActive(true);
        sfx.PlayOneShot(atkSFX);
    }

    public void UndoAtk()
    {
        atkArea.SetActive(false);
    }

    public void FollowAgain()
    {
        currentFollowDist = followDist * 5;
        awake = false;
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;

        if (dist >= atkDist * 2)
        {
            awake = true;
        }
            

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
