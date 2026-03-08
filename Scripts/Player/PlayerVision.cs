using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerVision : MonoBehaviour {
    [SerializeField] private Light2D playerLight;

    [SerializeField] private float maxRadius = 6f;
    [SerializeField] private float minRadius = 2f;

    [SerializeField] private float changeSpeed = 2f;

    private void Update()
    {
        if (MovePlayer.Instance.IsRunning())
        {
            playerLight.pointLightOuterRadius = Mathf.Lerp(
                playerLight.pointLightOuterRadius,
                maxRadius,
                changeSpeed * Time.deltaTime
            );
        }
        else
        {
            playerLight.pointLightOuterRadius = Mathf.Lerp(
                playerLight.pointLightOuterRadius,
                minRadius,
                changeSpeed * Time.deltaTime
            );
        }
    }
}