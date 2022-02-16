using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamZoom : MonoBehaviour
{
    public static CamZoom Instance { get; private set; }
    private float shakeTimer;
    public static bool ZoomActive;
    public CinemachineVirtualCamera Cam;
    public float Speed;

    private void Awake() {
        Instance = this;
    }
    
    void LateUpdate()
    {
        if(ZoomActive){
            Cam.m_Lens.OrthographicSize = Mathf.Lerp(Cam.m_Lens.OrthographicSize, 4, Speed * Time.deltaTime);
        } else {
            Cam.m_Lens.OrthographicSize = Mathf.Lerp(Cam.m_Lens.OrthographicSize, 6, Speed * Time.deltaTime);
        }
    }

    public void shakeCamera(float Intensity, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Intensity;
        shakeTimer = time;
    }

    private void Update() {
        if (shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
