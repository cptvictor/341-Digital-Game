using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField]
    private float coneAngleSize = 90f;

    private float remainingAngle;

    [SerializeField]
    private float turnSpeed = 3f;

    public GameObject bullet;

    [SerializeField]
    private float rateOfFire = 5f;

    private float fireTimer;

    [SerializeField]
    private float bulletVelocity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0;
        remainingAngle = coneAngleSize;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            fireTimer = rateOfFire;
            Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
            newBullet.setSpeed(bulletVelocity);
        }

        transform.Rotate(0, turnSpeed, 0);
        remainingAngle -= Mathf.Abs(turnSpeed);
        if(remainingAngle <= 0)
        {
            turnSpeed *= -1;
            remainingAngle = coneAngleSize;
        }
    }
}
