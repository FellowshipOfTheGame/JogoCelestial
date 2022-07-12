using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Limites do alcance da camera:")]
    public float maximoX;
    public float minimoX;
    public float maximoY;
    public float minimoY;

    [Header("Zoom:")]
    public Camera cameraFreeWalk;
    public float zoomIn;
    public float zoomOut;

    [Header("Player:")]
    public Transform Player;

    private void Awake()
    {
        cameraFreeWalk = GetComponent<Camera>();
        cameraFreeWalk.fieldOfView = zoomIn;
    }

    // Update is called once per frame
    static float x = 5.25f;
    static float y = 10.25f;
    void Update()
    {
        float fieldX = cameraFreeWalk.fieldOfView / x;
        float fieldY = cameraFreeWalk.fieldOfView / y;
        transform.position = new Vector3(Mathf.Clamp(Player.position.x, minimoX + fieldX, maximoX - fieldX), Mathf.Clamp(Player.position.y, minimoY + fieldY, maximoY - fieldY), transform.position.z);
    }
}
