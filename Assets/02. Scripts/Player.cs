using DG.Tweening.Core.Easing;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed = 10f;
    public float jumpSpeed = 5f;
    public float rotSpeed = 10f;

    public Vector3 dir = Vector3.zero;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    // ��/��/��/�� ����, A: 97, Z: 122, 26��
    // 4����Ű + ����Ű + �Ͻ�����Ű(Ÿ ������ esc)
    public int[] moveKeys;

    public PauseManager pauseManager;

    #region �� �Ҵ�
    void AssignObjects()
    {
        rigid = GetComponent<Rigidbody>();
        pauseManager = GameObject.Find("Pause Manager").GetComponent<PauseManager>();

        moveKeys = new int[6] { 'w', 'a', 's', 'd', 'j', 'p' };
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");
    }
    #endregion �� �Ҵ�

    private void Awake()
    {
        AssignObjects();
        //MakeRandomKeys();
    }

    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        CheckWall();
        rigid.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }

#region �̵� �� ȸ��
    void Move()
    {
        dir = Vector3.zero;

        // ���� �̵� (W)
        if (Input.GetKey((KeyCode)moveKeys[0])) { dir.z += 1; }
        // �Ʒ��� �̵� (S)
        if (Input.GetKey((KeyCode)moveKeys[2])) { dir.z -= 1; }
        // ���� �̵� (A)
        if (Input.GetKey((KeyCode)moveKeys[1])) { dir.x -= 1; }
        // ������ �̵� (D)
        if (Input.GetKey((KeyCode)moveKeys[3])) { dir.x += 1; }

        // ���� ����
        if (Input.GetKeyDown((KeyCode)moveKeys[4]) && CheckGround())
        {
            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        // �Ͻ����� ����
        if (Input.GetKeyDown((KeyCode)moveKeys[5]))
        {
            pauseManager.PauseGame();
        }

        dir.Normalize();

        // ȸ��
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }

    bool CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), dir, out hit, 0.5f, wallLayer))
        {
            speed = 0;
        }
        else
        {
            speed = 10f;
        }
    }

#endregion �̵� �� ȸ��

    void MakeRandomKeys()
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
