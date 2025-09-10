using UnityEngine;

namespace Polyperfect.Universal
{
    public class VictoryTree : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.PlayerWon();
            }
        }
    }
}
