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
    
    // ��/��/��/�� ����, A: 97, Z: 122, 26��
    public int[] moveKeys;

    #region �� �Ҵ�
    void AssignObjects()
    {
        characterController = GetComponent<CharacterController>();
        moveKeys = new int[4] { 'w', 'a', 's', 'd' };

        characterBody = transform.GetChild(0);
        cameraArm = transform.GetChild(1);
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
        LookAround();
    }

    #region �̵� �� ȸ��
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

        Vector3 lookForward = new(cameraArm.forward.x, 0f, cameraArm.forward.z);
        Vector3 lookRight = new(cameraArm.right.x, 0f, cameraArm.right.z);

        Vector3 moveDirection = vertical * lookForward + horizontal * lookRight;

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

    void LookAround()
    {
        // https://makerejoicegames.tistory.com/131
        // https://wergia.tistory.com/230
        mouseX += Input.GetAxis("Mouse X") * speed;

        mouseY += Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        cameraArm.transform.localEulerAngles = new(-mouseY, mouseX, 0);
        characterBody.transform.localEulerAngles = new(0, mouseX, 0);
    }
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
