using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Move")]
    public bool canMove = true;
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    Vector3 startPos;
    int direction = 1;

    [Header("Rotate")]
    public bool canRotate;
    public float rotateSpeed;
   
    
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (canMove)
            Move();

        if (canRotate)
            Rotate();
    }

    void Move()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        float distance = Vector3.Distance(startPos, transform.position);

        if (distance >= moveDistance)
        {
            direction *= -1; // Change Direction
        }
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

}
