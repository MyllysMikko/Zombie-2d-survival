using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] GameObject player;
    [SerializeField] PlayerController player;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        TurnTowardPlayer();
        MoveForward();
        

    }

    void MoveForward()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    void TurnTowardPlayer()
    {

        Vector3 toTarget = player.transform.position - transform.position;
        Vector3 rotatedToTarget = Quaternion.Euler(0, 0, 90) * toTarget;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
