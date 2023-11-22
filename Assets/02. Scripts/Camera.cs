using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float mouseX = 0;
    float mouseY = 0;
    public float turnSpeed = 10f;

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        // transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
        // transform.Rotate(Mathf.Clamp(-Input.GetAxis("Mouse Y") * speed, -55.0f, 55.0f), 0f, 0f);
        // https://makerejoicegames.tistory.com/131
        mouseX += Input.GetAxis("Mouse X") * turnSpeed;

        mouseY += Input.GetAxis("Mouse Y") * turnSpeed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        transform.localEulerAngles = new(-mouseY, mouseX, 0);
    }
}
