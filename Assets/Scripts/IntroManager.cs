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
        public string gameSceneName = "SampleScene"; // ���� ���� �� �̸�                                           // ���� ���� �� �̸�
        public float descriptionDuration = 3f;     // ���� �����ִ� �ð�
        public float countdownInterval = 1f;       // ī��Ʈ�ٿ� ����

        private void Start()
        {
            StartCoroutine(PlayIntroSequence());
        }

        private IEnumerator PlayIntroSequence()
        {
            // 1. ���� ���� ǥ��
            descriptionText.text = "�� ������ �̷� �����Դϴ�.\n���ƿ��� �Ѿ˵��� ���ϸ�\n���� �������� ������ ������.";
            countdownText.text = "";
            yield return new WaitForSeconds(descriptionDuration);

            // ī��Ʈ�ٿ� ���� ���� ���� ���� ����
            descriptionText.text = "";  // �Ǵ� descriptionText.gameObject.SetActive(false);


            // 2. ī��Ʈ�ٿ�
            for (int i = 3; i > 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSeconds(countdownInterval);
            }

            // 3. Start! ǥ��
            countdownText.text = "Start!";
            yield return new WaitForSeconds(1f);

            // 4. ���� ������ �̵�
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
