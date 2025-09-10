using UnityEngine;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        [Header("TPS Camera Reference")]
        public Transform tpsCamera;   // TPS 카메라 Transform 연결
        public float rotationSmooth = 10f; // 회전 부드럽게

        private Vector3 velocity;
        private bool isGrounded;

        void Update()
        {
            //  Ground Check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                controller.slopeLimit = 45f;
                velocity.y = -2f;
            }

            //  이동 입력
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (move.magnitude > 1)
                move /= move.magnitude;

            controller.Move(move * speed * Time.deltaTime);

            //  TPS 카메라 기준 Y축 회전 부드럽게
            if (move.magnitude > 0.01f && tpsCamera != null)
            {
                Vector3 camForward = tpsCamera.forward;
                camForward.y = 0;
                if (camForward.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(camForward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
                }
            }

            //  점프
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                controller.slopeLimit = 100f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //  중력 적용
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        // 즉사 기능
        public void TakeDamage()
        {
            Die();
        }

        private void Die()
        {
            Debug.Log("플레이어 사망!");
            Destroy(gameObject);
        }
    }
}
