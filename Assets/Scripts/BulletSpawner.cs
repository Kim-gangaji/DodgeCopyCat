using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform target;     // �÷��̾�
    private float spawnRate;
    private float timeAfterSpawn;

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // Player ã�� (�±װ� �ݵ�� "Player" �̾�� ��)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player ������Ʈ�� �������� �ʽ��ϴ�! Player �±׸� Ȯ���ϼ���.");
        }
    }

    void Update()
    {
        if (target == null) return;

        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            SpawnBullet();
            timeAfterSpawn = 0f;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    private void SpawnBullet()
    {
        // ������ ��ġ���� �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // �Ѿ��� �÷��̾� ������ ���ϰ� ȸ��
        bullet.transform.LookAt(target.position);
    }
}
