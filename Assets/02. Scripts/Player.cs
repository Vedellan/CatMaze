using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed = 1000f;
    public float jumpSpeed = 10f;
    public float yVelocity = 0;
    
    // ��/��/��/�� ����, A: 97, Z: 122, 26��
    public int[] moveKeys;

    public bool isGrounded = true;

    #region �� �Ҵ�
    void AssignObjects()
    {
        rigid = GetComponent<Rigidbody>();

        moveKeys = new int[4] { 'w', 'a', 's', 'd' };
    }
    #endregion �� �Ҵ�

    private void Awake()
    {
        AssignObjects();
        //MakeRandomMoveKeys();
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }
    }


    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    #region �̵� �� ȸ��
    void Move()
    {
        // https://acredev.tistory.com/18
        // https://wergia.tistory.com/230
        float horizontal = 0;
        float vertical = 0;

        // ���� �̵� (W)
        if (Input.GetKey((KeyCode)moveKeys[0]))
        {
            Debug.Log((KeyCode)moveKeys[0]);
            vertical += 1;
        }
        // �Ʒ��� �̵� (S)
        if (Input.GetKey((KeyCode)moveKeys[2]))
        {
            Debug.Log((KeyCode)moveKeys[1]);
            vertical -= 1;
        }
        // ���� �̵� (A)
        if (Input.GetKey((KeyCode)moveKeys[1]))
        {
            Debug.Log((KeyCode)moveKeys[2]);
            horizontal -= 1;
        }
        // ������ �̵� (D)
        if (Input.GetKey((KeyCode)moveKeys[3]))
        {
            Debug.Log((KeyCode)moveKeys[3]);
            horizontal += 1;
        }

        // ���� ����
        if (isGrounded)
        {
            yVelocity = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        Vector3 moveDirection = vertical * Vector3.forward + horizontal * Vector3.right;

        moveDirection.y = yVelocity;

        //transform.Translate(speed * Time.deltaTime * moveDirection);
        rigid.AddForce(speed * Time.deltaTime * moveDirection);
    }

/*    void LookAround()
    {
        // https://makerejoicegames.tistory.com/131
        mouseX += Input.GetAxis("Mouse X") * speed;

        mouseY += Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        cameraArm.transform.localEulerAngles = new(-mouseY, mouseX, 0);
    }*/
#endregion �̵� �� ȸ��

    void MakeRandomMoveKeys()
    {
        for(int i = 0; i < moveKeys.Length; ++i)
        {
            moveKeys[i] = Random.Range(97, 122);

            for(int j = 0; j < i; ++j)
            {
                // �ߺ��� Ű�� �Ҵ�� ���
                if (moveKeys[i] == moveKeys[j])
                {
                    --i;
                    break;
                }
            }

            // i�� 1 �����߱⿡ �ٽ� ����ȴ�.
        }
    }
}
