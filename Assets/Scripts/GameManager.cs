using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// PlayerMovement ��ũ��Ʈ�� ������ ���ӽ����̽��� �����ݴϴ�.
namespace Polyperfect.Universal
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // �̱���

        [Header("UI Elements")]
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI resultText;

        private float timer;
        private bool isGameActive = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            timer = 0f;
            resultText.text = "";
        }

        void Update()
        {
            if (isGameActive)
            {
                timer += Time.deltaTime;
                timerText.text = "Time: " + Mathf.FloorToInt(timer);

            }

            // Ű �Է����� �����
            if (!isGameActive)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R))
                {
                    RestartGame();
                }
            }
        }

        public void PlayerDied()
        {
            if (!isGameActive) return;
            isGameActive = false;
            resultText.text = "Game Over! Press R to Retry";
        }

        public void PlayerWon()
        {
            if (!isGameActive) return;
            isGameActive = false;

            // �ִ� ��� ����
            float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (timer < bestTime)
            {
                bestTime = timer;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }

            resultText.text = $"Victory! Time: {timer:F2}\nBest: {bestTime:F2}\nPress Enter to Restart";
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}