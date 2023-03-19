using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSway : MonoBehaviour
{
    [SerializeField] private float minSway;
    [SerializeField] private float maxSway;
    [SerializeField] private float smoothSway;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * minSway; // Valor do "input" é negativo para simular o efeito da inércia
        float moveY = -Input.GetAxis("Mouse Y") * minSway;

        moveX = Mathf.Clamp(moveX, -maxSway, maxSway); // Recebe um valor entre o mínimo (-maxSway) e máximo (maxSway)
        moveY = Mathf.Clamp(moveY, -maxSway, maxSway);

        Vector3 newPos = new Vector3(moveX, moveY, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos + initialPos, Time.deltaTime * smoothSway);
    }
}
