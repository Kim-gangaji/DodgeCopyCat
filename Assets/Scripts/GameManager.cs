using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// PlayerMovement 스크립트와 동일한 네임스페이스로 묶어줍니다.
namespace Polyperfect.Universal
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // 싱글톤

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

            // 키 입력으로 재시작
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

            // 최단 기록 저장
            float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (timer < bestTime)
            {
                bestTime = timer;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }

            // 정수로 변환해서 출력
            int currentTime = Mathf.FloorToInt(timer);      // 또는 RoundToInt
            int bestTimeInt = Mathf.FloorToInt(bestTime);   // 또는 RoundToInt

            resultText.text = $"Victory! \nTime: {currentTime}\nBest: {bestTimeInt}\nPress Enter to Restart";
        }


        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}