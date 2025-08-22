using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraStackSetup : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera weaponCam;

    void Awake()
    {
        //ȷ��������� Base
        var mainData = mainCam.GetUniversalAdditionalCameraData();
        mainData.renderType = CameraRenderType.Base;

        //ȷ����������� Overlay
        var weaponData = weaponCam.GetUniversalAdditionalCameraData();
        weaponData.renderType = CameraRenderType.Overlay;

        //�������������������� Stack
        if (!mainData.cameraStack.Contains(weaponCam))
            mainData.cameraStack.Add(weaponCam);
    }
}
