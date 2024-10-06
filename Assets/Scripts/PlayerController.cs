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

    void Start()
    {
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
            transform.position += new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;

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
}