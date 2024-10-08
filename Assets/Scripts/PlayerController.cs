using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health = 3;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMovementDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            return;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            // Поворот персонажа в направлении движения
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Сохраняем последнее направление движения
            lastMovementDirection = movement;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Логика стрельбы
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        rb.angularVelocity = 0; // Остановить вращение каждый кадр физики
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetDirection(lastMovementDirection);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Slow")) {
            moveSpeed = 2f;
        }
        if (other.CompareTag("Lethal")) {
            health = 0;
            rb.velocity = Vector2.zero;
        }

    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Slow")) {
            moveSpeed = 5f;
        }
    }
}
