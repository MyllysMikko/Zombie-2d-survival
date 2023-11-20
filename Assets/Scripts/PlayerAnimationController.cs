using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Feet")]
    [SerializeField] float feetTurnSpeed;
    [SerializeField] GameObject feet;
    [SerializeField] Animator feetAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimateFeet(Vector3 direction)
    {

        if (direction.magnitude > 0.1f)
        {
            direction.z = transform.position.z;

            Vector3 rotatedToTarget = Quaternion.Euler(0, 0, 90) * direction;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, feetTurnSpeed * Time.deltaTime);

            feetAnim.SetBool("running", true);
        }
        else
        {
            feetAnim.SetBool("running", false);
        }
    }
}