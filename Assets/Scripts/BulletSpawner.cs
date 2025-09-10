using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // Player 찾기 (씬에서 태그 "Player" 붙여야 함)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player 오브젝트가 존재하지 않습니다! 태그를 확인하세요.");
        }
    }

    void Update()
    {
        if (target == null) return;

        timeAfterSpawn += Time.deltaTime;

        // spawnRate 이상이면 총알 생성
        if (timeAfterSpawn >= spawnRate)
        {
            SpawnBullet();
            timeAfterSpawn = 0f; // 시간 초기화
            spawnRate = Random.Range(spawnRateMin, spawnRateMax); // 다음 생성 간격 설정
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        // 탄알 방향을 플레이어 쪽으로
        bullet.transform.LookAt(target.position);
    }
}

