using UnityEngine;
using Polyperfect.Universal; // PlayerMovement 네임스페이스

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;  // 총알 존재 시간
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Destroy(gameObject, lifeTime); // 일정 시간 뒤 삭제
    }

    void OnCollisionEnter(Collision collision)
    {
        // 플레이어 맞으면 즉사 처리
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(); // 즉사
            }
            Destroy(gameObject); // 맞으면 삭제
        }

        // 벽이나 다른 오브젝트에 맞으면 물리 튕김
        // Rigidbody + Physic Material로 자동 반사
        // 필요시 속도 보정:
        // rb.velocity = rb.velocity.magnitude * rb.velocity.normalized; // 튕김 후 속도 일정하게 유지
    }
}
