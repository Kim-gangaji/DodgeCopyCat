using UnityEngine;

public class TPSFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player;          // �÷��̾� Transform
    public Transform playerBody;      // �÷��̾� Y�� ȸ����

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 1.2f, -3f);
    public float smoothSpeed = 10f;        // ī�޶� �̵� �ε巯��
    public float mouseSensitivity = 3f;    // ���콺 �¿� �ΰ���
    public float xRotation = 24f;          // X�� ���� ����

    private float currentYaw = 0f;         // ī�޶� Y�� ȸ�� ����
    private Vector3 velocity;

    void LateUpdate()
    {
        if (!player) return;

        // ���콺 �Է�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYaw += mouseX;

        // ī�޶� ��ġ ���
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 desiredPos = player.position + rotation * offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, 1f / smoothSpeed);

        // ī�޶� ȸ��
        transform.rotation = Quaternion.Euler(xRotation, currentYaw, 0);

        // �÷��̾� Y�� ȸ�� �ε巴�� ����
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

    // TPS ��ȯ �� ��� ��ġ/ȸ�� ����
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
