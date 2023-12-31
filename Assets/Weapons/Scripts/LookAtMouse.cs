using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] Transform shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetLocation.z = transform.position.z;

        Vector3 toTarget = targetLocation - shootPoint.transform.position;
        Vector3 rotatedToTarget = Quaternion.Euler(0, 0, 90) * toTarget;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);



    }
}
