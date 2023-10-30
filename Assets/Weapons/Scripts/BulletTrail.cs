using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{

    Vector3 target;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;

        float distance = Vector3.Distance(transform.position, target);

        speed = distance / 0.1f;
    }
}
