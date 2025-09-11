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
            resultText.text = "Game Over! \nPress R to Retry";
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

            // ������ ��ȯ�ؼ� ���
            int currentTime = Mathf.FloorToInt(timer);      // �Ǵ� RoundToInt
            int bestTimeInt = Mathf.FloorToInt(bestTime);   // �Ǵ� RoundToInt

            resultText.text = $"Victory! \nTime: {currentTime}\nBest: {bestTimeInt}\nPress Enter to Restart";
        }


        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}