using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    public float speed = 10f;
    public float jumpSpeed = 10f;
    CharacterController characterController;

    float mouseX = 0;
    float mouseY = 0;

    public float gravity = -20f;
    public float yVelocity = 0;
    
    // 상/하/좌/우 순서, A: 97, Z: 122, 26개
    public int[] moveKeys;

    #region 값 할당
    void AssignObjects()
    {
        characterController = GetComponent<CharacterController>();
        moveKeys = new int[4] { 'w', 'a', 's', 'd' };

        characterBody = transform.GetChild(0);
        cameraArm = transform.GetChild(1);
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
        LookAround();
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
        if (characterController.isGrounded)
        {
            yVelocity = 0;

            if (Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        // 방향 조정
        Vector3 lookForward = new(cameraArm.forward.x, 0f, cameraArm.forward.z);
        Vector3 lookRight = new(cameraArm.right.x, 0f, cameraArm.right.z);

        Vector3 moveDirection = vertical * lookForward + horizontal * lookRight;

        yVelocity += (gravity * Time.deltaTime);

        moveDirection.y = yVelocity;

        characterBody.forward = lookForward;
        characterController.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
    }

    void LookAround()
    {
        // https://makerejoicegames.tistory.com/131
        mouseX += Input.GetAxis("Mouse X") * speed;

        mouseY += Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        cameraArm.transform.localEulerAngles = new(-mouseY, mouseX, 0);
    }
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
