using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{




    #region Auxiliary data
    [Header("Sesitivity mouse")]
    [SerializeField] private float _horizontalSpeed = 1.0f;

    [SerializeField] private float _verticalSpeed = 1.0f;

    [SerializeField] private float _clampAngle = 80.0f;
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
               // if (playerInputController == null) playerInputController = PlayerInputController.Instance;
                if (playerInputController == null) return;
                Vector2 delta = playerInputController.GetMouseDelta();//ver como coger las 3 acciones
                startingRotation.x += delta.x * Time.deltaTime;
                startingRotation.y += delta.y * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -_clampAngle, _clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
