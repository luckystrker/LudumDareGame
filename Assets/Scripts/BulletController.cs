using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 0.5f;

    private Vector2 direction;
    private SpriteRenderer spriteRenderer;
    private float elapsedTime = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Уменьшение прозрачности
        elapsedTime += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / lifeTime);
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Логика при столкновении с другими объектами
        Destroy(gameObject);
    }
}