using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private CamCtrl camCtrl;
    
    [Header("Properties")]
    private Animator anim;
    private AudioSource sfx;

    [Header("Weapon")]
    [SerializeField] private float bulletRange = 100f;
    public int magazineCapacity = 0; // Capacidade de balas no pente
    public int bulletsInMagazine; // Número de balas no pente
    public int bulletsLeft; // Total de balas
    public float noAimingSpread;
    public float aimingSpread;
    private float currentSpread;
    [SerializeField] private AudioClip reloadSFX;

    [SerializeField] private float fireRate = 0.7f;
    [SerializeField] private float fireTimer;
    private bool isReloading;

    [Header("Aim")]
    [SerializeField] private GameObject aimLight;
    [SerializeField] private List<GameObject> aimBulletsList;
    public bool isAiming;

    [Header("Shot FX")]
    public Transform shootPoint;
    public ParticleSystem fireFX;
    public GameObject hitFX;
    public GameObject bulletImpact;
    [SerializeField] private AudioClip pistolShotSFX;
    [SerializeField] private AudioClip ammoSFX;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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

        if(fireTimer < fireRate && isReloading == false)
        {
            fireTimer += Time.deltaTime;
        }

        if (Time.timeScale == 1f)
        {
            ToAim();
        }
    }

    private void FixedUpdate()
    {
        //AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        //isReloading = info.IsName("Reload"); // "isReloading" recebe o valor da animação // Uma alternativa para controlar a animação (entender melhor depois)
    }

    private void Fire()
    {
        if (fireTimer < fireRate || isReloading || bulletsInMagazine <= 0 || Time.timeScale == 0f)
        {
            return;
        }

        // Simula a tragetória do disparo
        RaycastHit hit;

        Vector3 shootDirection = shootPoint.transform.forward;
        shootDirection = shootDirection + shootPoint.TransformDirection(new Vector3(Random.Range(-currentSpread, currentSpread),
            Random.Range(-currentSpread, currentSpread)));

        if (Physics.Raycast(shootPoint.position, shootDirection, out hit, bulletRange))
        {
            GameObject hitParticle = Instantiate(hitFX, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); // Gera o efeito de faisca no local que a bala acertou
            GameObject bullet = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)); // Gera uma marca de bala no local que a bala acertou
            bullet.transform.SetParent(hit.transform); // Gera a marca da bala dentro do objeto que ela atingir

            Destroy(hitParticle, 1f);
            Destroy(bullet, 4f);

            if (hit.transform.GetComponent<Enemy>())
            {
                Destroy(bullet);
                hit.transform.GetComponent<Enemy>().ApplyDamage(damage);
            }

            if (hit.transform.GetComponent<ObjectGeren>())
            {
                Destroy(bullet);
            }
        }

        if (isAiming)
        {
            anim.CrossFadeInFixedTime("AimingFire", 0.01f); //"Chama" a animação diretamente via script e determina a sua duração
        }
        else
        {
            anim.CrossFadeInFixedTime("Fire", 0.01f);
        }

        fireFX.Play();
        sfx.PlayOneShot(pistolShotSFX);
        bulletsInMagazine--;
        fireTimer = 0f;
    }

    public void ToAim()
    {
        if (Input.GetButton("Fire2") && isReloading == false)
        {
            anim.SetBool("Aiming", true);
            isAiming = true;
            aimLight.SetActive(true);
            camCtrl.ZoomIn(60f, 30f);
            currentSpread = aimingSpread;
        }
        else
        {
            anim.SetBool("Aiming", false);
            isAiming = false;
            aimLight.SetActive(false);
            camCtrl.ZoomOut(30f, 60f);
            currentSpread = noAimingSpread;
        }

        if (bulletsInMagazine > 11)
        {
            aimBulletsList[11].SetActive(true);
        }
        else { aimBulletsList[11].SetActive(false); }

        if (bulletsInMagazine > 10)
        {
            aimBulletsList[10].SetActive(true);
        }
        else { aimBulletsList[10].SetActive(false); }

        if (bulletsInMagazine > 9)
        {
            aimBulletsList[9].SetActive(true);
        }
        else { aimBulletsList[9].SetActive(false); }

        if (bulletsInMagazine > 8)
        {
            aimBulletsList[8].SetActive(true);
        }
        else { aimBulletsList[8].SetActive(false); }

        if (bulletsInMagazine > 7)
        {
            aimBulletsList[7].SetActive(true);
        }
        else { aimBulletsList[7].SetActive(false); }

        if (bulletsInMagazine > 6)
        {
            aimBulletsList[6].SetActive(true);
        }
        else { aimBulletsList[6].SetActive(false); }

        if (bulletsInMagazine > 5)
        {
            aimBulletsList[5].SetActive(true);
        }
        else { aimBulletsList[5].SetActive(false); }

        if (bulletsInMagazine > 4)
        {
            aimBulletsList[4].SetActive(true);
        }
        else { aimBulletsList[4].SetActive(false); }

        if (bulletsInMagazine > 3)
        {
            aimBulletsList[3].SetActive(true);
        }
        else { aimBulletsList[3].SetActive(false); }

        if (bulletsInMagazine > 2)
        {
            aimBulletsList[2].SetActive(true);
        }
        else { aimBulletsList[2].SetActive(false); }

        if (bulletsInMagazine > 1)
        {
            aimBulletsList[1].SetActive(true);
        }
        else { aimBulletsList[1].SetActive(false); }

        if (bulletsInMagazine > 0)
        {
            aimBulletsList[0].SetActive(true);
        }
        else { aimBulletsList[0].SetActive(false); }
    }

    private void DoReload()
    {
        if (isReloading)
        {
            return;
        }

        isReloading = true;
        sfx.PlayOneShot(reloadSFX);
        anim.SetTrigger("Recharge");
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
        isReloading = false;
    }

    public void CollectAmmo(int ammoAdd)
    {
        bulletsLeft = bulletsLeft + ammoAdd;
        sfx.PlayOneShot(ammoSFX);
    }
}
