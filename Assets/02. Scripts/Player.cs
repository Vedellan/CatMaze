using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed = 10f;
    public float jumpSpeed = 5f;
    public float rotSpeed = 10f;

    public Vector3 dir = Vector3.zero;

    public LayerMask groundLayer;

    // ��/��/��/�� ����, A: 97, Z: 122, 26��
    public int[] moveKeys;

    #region �� �Ҵ�
    void AssignObjects()
    {
        rigid = GetComponent<Rigidbody>();

        moveKeys = new int[4] { 'w', 'a', 's', 'd' };
        groundLayer = LayerMask.GetMask("Ground");
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
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
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

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + dir * speed * Time.deltaTime);
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
