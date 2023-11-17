using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
