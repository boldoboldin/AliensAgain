using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private Rigidbody rb;
    private bool pausedGame;
    
    [SerializeField] private Transform cameraPos;

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject pauseLight;

    [SerializeField] private float playerVel;
    [SerializeField] private int playerHP;

    [SerializeField] private AudioSource footStepSfx;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Oculta o cursor do mouse e limita sua área à janela inGame
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pausedGame = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (pausedGame == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausedGame = true;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            pauseLight.SetActive(true);
            footStepSfx.enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausedGame = false;
            pauseScreen.SetActive(false);
            pauseLight.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraPos.eulerAngles.y, 0), 100 * Time.deltaTime);
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (weapon.isAiming == false))
        {
            playerVel = 10;
            footStepSfx.pitch = 0.7f;
        }
        else
        {
            playerVel = 5;
            footStepSfx.pitch = 0.5f;;
        }

        if (weapon.isAiming == true)
        {
            playerVel = 2;
            footStepSfx.pitch = 0f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.TransformVector(new Vector3(moveX, 0, -moveZ));

        rb.MovePosition(rb.position + direction * playerVel * Time.deltaTime);

        if ((moveX != 0f) || (moveZ != 0f))
        {
            footStepSfx.enabled = true;
        }
        else
        {
            footStepSfx.enabled = false;
        }
    }
}
