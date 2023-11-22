using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;
    CharacterController characterController;

    float mouseX = 0;
    float mouseY = 0;

    public float gravity = -20f;
    public float yVelocity = 0;

    // ��/��/��/�� ����, A: 97, Z: 122, 26��
    public int[] moveKeys;

    #region �� �Ҵ�
    void AssignObjects()
    {
        characterController = GetComponent<CharacterController>();
        moveKeys = new int[4] { 'w', 'a', 's', 'd' };
    }
    #endregion �� �Ҵ�

    private void Awake()
    {
        AssignObjects();
        MakeRandomMoveKeys();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        // https://acredev.tistory.com/18
        float horizontal = 0;
        float vertical = 0;

        // ���� �̵�
        if (Input.GetKey((KeyCode)moveKeys[0]))
        {
            Debug.Log((KeyCode)moveKeys[0]);
            horizontal += 1;
        }
        // �Ʒ��� �̵�
        if (Input.GetKey((KeyCode)moveKeys[1]))
        {
            Debug.Log((KeyCode)moveKeys[1]);
            horizontal -= 1;
        }
        // ���� �̵�
        if (Input.GetKey((KeyCode)moveKeys[2]))
        {
            Debug.Log((KeyCode)moveKeys[2]);
            vertical -= 1;
        }
        // ������ �̵�
        if (Input.GetKey((KeyCode)moveKeys[3]))
        {
            Debug.Log((KeyCode)moveKeys[3]);
            vertical += 1;
        }

        Vector3 moveDirection = new(horizontal, 0, vertical);

        if (characterController.isGrounded)
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

    void MakeRandomMoveKeys()
    {
        for(int i = 0; i < moveKeys.Length; i++)
        {
            moveKeys[i] = Random.Range(97, 122);

            for(int j = 0; j < i; j++)
            {
                // �ߺ��� Ű�� �Ҵ�� ���
                if (moveKeys[i] == moveKeys[j])
                {
                    i--;
                    break;
                }
            }

            // i�� 1 �����߱⿡ �ٽ� ����ȴ�.
        }
    }
}
