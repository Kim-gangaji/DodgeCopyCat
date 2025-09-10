using UnityEngine;
using Polyperfect.Universal; // PlayerMovement ���ӽ����̽�

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;  // �Ѿ� ���� �ð�
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Destroy(gameObject, lifeTime); // ���� �ð� �� ����
    }

    void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� ������ ��� ó��
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(); // ���
            }
            Destroy(gameObject); // ������ ����
        }

        // ���̳� �ٸ� ������Ʈ�� ������ ���� ƨ��
        // Rigidbody + Physic Material�� �ڵ� �ݻ�
        // �ʿ�� �ӵ� ����:
        // rb.velocity = rb.velocity.magnitude * rb.velocity.normalized; // ƨ�� �� �ӵ� �����ϰ� ����
    }
}
