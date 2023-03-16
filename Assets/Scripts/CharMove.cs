using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float velChar;
    [SerializeField] private int hp;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velChar = 6;
        }
        else
        {
            velChar = 3;
        }


        float velZ = Input.GetAxis("Vertical") * velChar;
        float velX = Input.GetAxis("Horizontal") * velChar;

        //PosCorrigida
        Vector3 velCorrigida = velX * transform.right + velZ * transform.forward;

        rb.velocity = new Vector3(velCorrigida.x, rb.velocity.y, velCorrigida.z);

        //Movimento Girar
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

}
