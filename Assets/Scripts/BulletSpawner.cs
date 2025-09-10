using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform target;     // 플레이어
    private float spawnRate;
    private float timeAfterSpawn;

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // Player 찾기 (태그가 반드시 "Player" 이어야 함)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player 오브젝트가 존재하지 않습니다! Player 태그를 확인하세요.");
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
        // 스포너 위치에서 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 총알을 플레이어 쪽으로 향하게 회전
        bullet.transform.LookAt(target.position);
    }
}
