using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject fpsCamera;
    public GameObject tpsCamera;
    public TPSFollow tpsFollow;

    private bool isFPS = true;

    void Start()
    {
        fpsCamera.SetActive(true);
        tpsCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFPS = !isFPS;
            fpsCamera.SetActive(isFPS);
            tpsCamera.SetActive(!isFPS);

            if (!isFPS && tpsFollow != null)
            {
                tpsFollow.ResetImmediate(); // TPS 카메라 바로 정렬
            }
        }
    }
}
