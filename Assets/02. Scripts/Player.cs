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

    // 상/하/좌/우 순서, A: 97, Z: 122, 26개
    // 4방향키 + 점프키 + 일시정지키(타 게임의 esc)
    public int[] moveKeys;

    public PauseManager pauseManager;

    #region 값 할당
    void AssignObjects()
    {
        rigid = GetComponent<Rigidbody>();
        pauseManager = GameObject.Find("Pause Manager").GetComponent<PauseManager>();

        moveKeys = new int[6] { 'w', 'a', 's', 'd', 'j', 'p' };
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");
    }
    #endregion 값 할당

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

#region 이동 및 회전
    void Move()
    {
        dir = Vector3.zero;

        // 위쪽 이동 (W)
        if (Input.GetKey((KeyCode)moveKeys[0])) { dir.z += 1; }
        // 아래쪽 이동 (S)
        if (Input.GetKey((KeyCode)moveKeys[2])) { dir.z -= 1; }
        // 왼쪽 이동 (A)
        if (Input.GetKey((KeyCode)moveKeys[1])) { dir.x -= 1; }
        // 오른쪽 이동 (D)
        if (Input.GetKey((KeyCode)moveKeys[3])) { dir.x += 1; }

        // 점프 로직
        if (Input.GetKeyDown((KeyCode)moveKeys[4]) && CheckGround())
        {
            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        // 일시정지 로직
        if (Input.GetKeyDown((KeyCode)moveKeys[5]))
        {
            pauseManager.PauseGame();
        }

        dir.Normalize();

        // 회전
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

#endregion 이동 및 회전

    void MakeRandomKeys()
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
