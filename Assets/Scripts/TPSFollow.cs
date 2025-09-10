using UnityEngine;

public class TPSFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player;          // 플레이어 Transform
    public Transform playerBody;      // 플레이어 Y축 회전용

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 1.2f, -3f);
    public float smoothSpeed = 10f;        // 카메라 이동 부드러움
    public float mouseSensitivity = 3f;    // 마우스 좌우 민감도
    public float xRotation = 24f;          // X축 각도 고정

    private float currentYaw = 0f;         // 카메라 Y축 회전 누적
    private Vector3 velocity;

    void LateUpdate()
    {
        if (!player) return;

        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYaw += mouseX;

        // 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 desiredPos = player.position + rotation * offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, 1f / smoothSpeed);

        // 카메라 회전
        transform.rotation = Quaternion.Euler(xRotation, currentYaw, 0);

        // 플레이어 Y축 회전 부드럽게 적용
        if (playerBody != null)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            if (forward.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(forward);
                playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRot, smoothSpeed * Time.deltaTime);
            }
        }
    }

    // TPS 전환 시 즉시 위치/회전 보정
    public void ResetImmediate()
    {
        if (!player) return;

        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        transform.position = player.position + rotation * offset;
        transform.rotation = Quaternion.Euler(xRotation, currentYaw, 0);
        velocity = Vector3.zero;

        if (playerBody != null)
        {
            Vector3 forward = transform.forward;
            forward.y = 0f;
            playerBody.rotation = Quaternion.LookRotation(forward);
        }
    }
}
