using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float edgePercentage = 0.02f;
    [SerializeField] float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.mousePosition.x / Screen.width;
        float mouseY = Input.mousePosition.y / Screen.height;

        float xMultiplier = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(edgePercentage, 1 - edgePercentage, mouseX));
        float yMultiplier = Mathf.Lerp(-1, 1f, Mathf.InverseLerp(edgePercentage, 1 - edgePercentage, mouseY));

        //Debug.Log($"{xMultiplier}, {yMultiplier}");

        Vector3 cameraPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        cameraPos.x += cameraOffset.x * xMultiplier;
        cameraPos.y += cameraOffset.y * yMultiplier;


        //transform.position = player.transform.position;

        transform.position = Vector3.Lerp(transform.position, cameraPos, cameraSpeed * Time.deltaTime);

    }
}
