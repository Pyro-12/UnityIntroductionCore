using UnityEngine;
using System.Collections;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private float count;
    public TextMeshProUGUI txtFPS;

    private IEnumerator Start()
    {
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {

        if (txtFPS != null)
        {
            txtFPS.text = "FPS: " + Mathf.Round(count);
        }
    }
}
