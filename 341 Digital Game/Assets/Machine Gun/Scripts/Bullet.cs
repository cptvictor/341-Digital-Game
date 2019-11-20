using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;

    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
        transform.position += forwardMovement;
    }

     void OnTriggerEnter(Collider other)
    {
        CharacterControls soldier = other.gameObject.GetComponent<CharacterControls>();
        if(soldier != null)
        {
            soldier.Die();
        }
        Destroy(this.gameObject);
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
