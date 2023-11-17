using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;
    CharacterController characterController;

    float mouseX = 0;
    float mouseY = 0;

    public float gravity = -20f;
    public float yVelocity = 0;

    private void Update()
    {
        characterController = GetComponent<CharacterController>();

        Move();
        Rotate();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new(horizontal, 0, vertical);

        if(characterController.isGrounded)
        {
            yVelocity = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += (gravity * Time.deltaTime);

        moveDirection.y = yVelocity;

        characterController.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
    }

    void Rotate()
    {
        // transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
        // transform.Rotate(Mathf.Clamp(-Input.GetAxis("Mouse Y") * speed, -55.0f, 55.0f), 0f, 0f);
        // https://makerejoicegames.tistory.com/131
        mouseX += Input.GetAxis("Mouse X") * speed;

        mouseY += Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        transform.localEulerAngles = new(-mouseY, mouseX, 0);
    }
}
