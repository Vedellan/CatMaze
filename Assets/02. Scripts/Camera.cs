using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTr;
    Vector3 offset = new(0, 10, -2.5f);

    private void Awake()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = playerTr.position + offset;
    }
}
