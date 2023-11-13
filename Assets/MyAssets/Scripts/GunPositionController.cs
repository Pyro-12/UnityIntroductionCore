
using UnityEngine;

public class GunPositionController : MonoBehaviour
{
    #region Auxiliary data
    private Transform player;
    private Transform cinemachineCam;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = transform;
        cinemachineCam = GameObject.FindGameObjectWithTag("CinemachineCam").transform; // Asigna la cámara Cinemachine
    }

    // Update is called once per frame
    void Update()
    {
        if (cinemachineCam != null)
        {
            // Obtener la rotación de la cámara Cinemachine
            Quaternion camRotation = cinemachineCam.rotation;

            // Aplicar la rotación de la cámara al arma
            transform.rotation = camRotation;
        }
        else
        {
            Debug.LogError("CinemachineCam not found. Make sure it is tagged correctly.");
        }
    }
}
