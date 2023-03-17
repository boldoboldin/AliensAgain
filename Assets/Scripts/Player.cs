using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform cameraPos;

    [SerializeField] private float playerVel;
    [SerializeField] private int playerHP;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Oculta o cursor do mouse e limita sua área à janela inGame
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraPos.eulerAngles.y, 0), 100 * Time.deltaTime);
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerVel = 9;
        }
        else
        {
            playerVel = 5;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.TransformVector(new Vector3(moveX, 0, -moveZ));

        rb.MovePosition(rb.position + direction * playerVel * Time.deltaTime);
    }
}
