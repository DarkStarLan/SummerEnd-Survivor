using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraStackSetup : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera weaponCam;

    void Awake()
    {
        //确保主相机是 Base
        var mainData = mainCam.GetUniversalAdditionalCameraData();
        mainData.renderType = CameraRenderType.Base;

        //确保武器相机是 Overlay
        var weaponData = weaponCam.GetUniversalAdditionalCameraData();
        weaponData.renderType = CameraRenderType.Overlay;

        //把武器相机加入主相机的 Stack
        if (!mainData.cameraStack.Contains(weaponCam))
            mainData.cameraStack.Add(weaponCam);
    }
}
