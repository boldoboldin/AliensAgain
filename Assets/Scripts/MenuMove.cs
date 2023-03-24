using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMove : MonoBehaviour
{
    [SerializeField] private float velX;
    [SerializeField] private float velY;
    [SerializeField] private GameObject inicialPos;



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + velX, transform.position.y + velY, transform.position.z);

        if (transform.position.y >= 1595.82f)
        {
           transform.position = new Vector3(inicialPos.transform.position.x, -1114.2f, inicialPos.transform.position.z);
        }
    }
}
