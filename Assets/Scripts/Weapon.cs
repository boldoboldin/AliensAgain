using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField] private float bulletRange = 100f;
    public int magazineCapacity = 0; // Capacidade de balas no pente
    public int bulletsInMagazine; // Número de balas no pente
    public int bulletsLeft = 36; // Total de balas


    [SerializeField] private float fireRate = 0.7f;
    [SerializeField] private float fireTimer;

    public Transform shootPoint;
    public ParticleSystem fireFX;

    private bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
        
        if (Input.GetButton("Fire1") && bulletsInMagazine <= 0 && bulletsLeft > 0) // Atirar sem balas no pente recarrega automaticamente a arma caso ainda tenha munição
        {
            DoReload();
        }

        if (Input.GetKeyDown(KeyCode.R)) //Recarrega a arma caso ainda tenha munição
        {
            if (bulletsInMagazine < magazineCapacity && bulletsLeft > 0)
            {
                DoReload();
            }
        }

        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        isReloading = info.IsName("Reload"); // "isReloading" recebe o valor da animação
    }

    private void Fire()
    {
        if(fireTimer < fireRate || isReloading || bulletsInMagazine <=0)
        {
            return;
        }

        // Simula a tragetória do disparo
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, bulletRange))

        anim.CrossFadeInFixedTime("Fire", 0.01f); //"Chama" a animação diretamente via script e determina a sua duração
        fireFX.Play();
        bulletsInMagazine--;
        fireTimer = 0f;
    }

    private void DoReload()
    {
        if (isReloading)
        {
            return;
        }

        Reload();

        //anim.CrossFadeInFixedTime("Reload", 0.01f);
    }

    public void Reload()
    {
        if(bulletsLeft <= 0)
        {
            return;
        }

        int bulletsToLoad = magazineCapacity - bulletsInMagazine;
        int bulletsToDeduct;
        
        if (bulletsLeft >= bulletsToLoad)
        {
            bulletsToDeduct = bulletsToLoad; 
        }
        else
        {
            bulletsToDeduct = bulletsLeft; // Quando o número munição restante for menor que a capacidade de balas do pente, todas as balas restantes são carregadas
        }

        bulletsLeft -= bulletsToDeduct; // Ao carrecar a arma reduz a quantidade total de munição
        bulletsInMagazine += bulletsToDeduct; // Ao carrecar aumenta a quantidade de balas no pente
    }
}
