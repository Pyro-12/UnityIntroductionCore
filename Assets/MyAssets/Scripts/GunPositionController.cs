
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
            Vector3 camRotation = cinemachineCam.localRotation.eulerAngles;

            // Aplicar la rotación de la cámara en todos los ejes al arma
            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        }
        else
        {
            Debug.LogError("CinemachineCam not found. Make sure it is tagged correctly.");
        }
    }
}
