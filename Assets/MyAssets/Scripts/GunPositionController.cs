
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
        cinemachineCam = GameObject.FindGameObjectWithTag("CinemachineCam").transform; // Asigna la c�mara Cinemachine
    }

    // Update is called once per frame
    void Update()
    {
        if (cinemachineCam != null)
        {
            // Obtener la rotaci�n de la c�mara Cinemachine
            Quaternion camRotation = cinemachineCam.rotation;

            // Aplicar la rotaci�n de la c�mara al arma
            transform.rotation = camRotation;
        }
        else
        {
            Debug.LogError("CinemachineCam not found. Make sure it is tagged correctly.");
        }
    }
}
