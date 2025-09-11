using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Polyperfect.Universal
{
    public class IntroManager : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI countdownText;

        [Header("Game Settings")]
        public string gameSceneName = "SampleScene"; // 실제 게임 씬 이름                                           // 실제 게임 씬 이름
        public float descriptionDuration = 3f;     // 설명 보여주는 시간
        public float countdownInterval = 1f;       // 카운트다운 간격

        private void Start()
        {
            StartCoroutine(PlayIntroSequence());
        }

        private IEnumerator PlayIntroSequence()
        {
            // 1. 설명 문구 표시
            descriptionText.text = "이 게임은 미로 게임입니다.\n날아오는 총알들을 피하며\n골인 지점까지 도달해 보세요.";
            countdownText.text = "";
            yield return new WaitForSeconds(descriptionDuration);

            // 카운트다운 시작 전에 설명 문구 비우기
            descriptionText.text = "";  // 또는 descriptionText.gameObject.SetActive(false);


            // 2. 카운트다운
            for (int i = 3; i > 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSeconds(countdownInterval);
            }

            // 3. Start! 표시
            countdownText.text = "Start!";
            yield return new WaitForSeconds(1f);

            // 4. 게임 씬으로 이동
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
