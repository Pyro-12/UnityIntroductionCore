using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    #region Auxiliary data
    [Header("Sesitivity mouse")]
    [SerializeField] private float horizontalSpeed = 1.0f;

    [SerializeField] private float verticalSpeed = 1.0f;

    [SerializeField] private float clampAngle = 80.0f;
    private PlayerInputController playerInputController;
    private Vector3 startingRotation;
    #endregion

    protected override void Awake()
    {
        //playerInputController = PlayerInputController.Instance; 
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                if (playerInputController == null) return;
                Vector2 delta = playerInputController.GetMouseDelta();
                startingRotation.x += delta.x * Time.deltaTime * verticalSpeed;
                startingRotation.y += delta.y * Time.deltaTime * horizontalSpeed;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
