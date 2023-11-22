using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed = 1000f;
    public float jumpSpeed = 10f;
    public float yVelocity = 0;
    
    // 상/하/좌/우 순서, A: 97, Z: 122, 26개
    public int[] moveKeys;

    public bool isGrounded = true;

    #region 값 할당
    void AssignObjects()
    {
        rigid = GetComponent<Rigidbody>();

        moveKeys = new int[4] { 'w', 'a', 's', 'd' };
    }
    #endregion 값 할당

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

    #region 이동 및 회전
    void Move()
    {
        // https://acredev.tistory.com/18
        // https://wergia.tistory.com/230
        float horizontal = 0;
        float vertical = 0;

        // 위쪽 이동 (W)
        if (Input.GetKey((KeyCode)moveKeys[0]))
        {
            Debug.Log((KeyCode)moveKeys[0]);
            vertical += 1;
        }
        // 아래쪽 이동 (S)
        if (Input.GetKey((KeyCode)moveKeys[2]))
        {
            Debug.Log((KeyCode)moveKeys[1]);
            vertical -= 1;
        }
        // 왼쪽 이동 (A)
        if (Input.GetKey((KeyCode)moveKeys[1]))
        {
            Debug.Log((KeyCode)moveKeys[2]);
            horizontal -= 1;
        }
        // 오른쪽 이동 (D)
        if (Input.GetKey((KeyCode)moveKeys[3]))
        {
            Debug.Log((KeyCode)moveKeys[3]);
            horizontal += 1;
        }

        // 점프 로직
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
#endregion 이동 및 회전

    void MakeRandomMoveKeys()
    {
        for(int i = 0; i < moveKeys.Length; ++i)
        {
            moveKeys[i] = Random.Range(97, 122);

            for(int j = 0; j < i; ++j)
            {
                // 중복된 키가 할당된 경우
                if (moveKeys[i] == moveKeys[j])
                {
                    --i;
                    break;
                }
            }

            // i가 1 감소했기에 다시 실행된다.
        }
    }
}
