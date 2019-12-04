using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInput : MonoBehaviour
{
    [SerializeField]
    private bool inTrench;

    public float moveTime = 2f;

    private float moveTimer;

    public float stanceTime = 2f;

    private float stanceTimer;

    public float fireTime = 0.7f;

    private float fireTimer;

    private CharacterControls charControl;

    void Start()
    {
        moveTimer = moveTime;
        fireTimer = fireTime;
        stanceTimer = stanceTime;

        charControl = GetComponent<CharacterControls>();
        
        if(inTrench)
        {
            charControl.Jump();
        }
        charControl.SendDirInput(new Vector2(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;
        if(moveTimer <= 0)
        {
            chooseDirection();
            moveTimer = moveTime;
        }

        stanceTimer -= Time.deltaTime;
        if(stanceTimer <= 0)
        {
            chooseStance();
            stanceTimer = stanceTime;
        }

        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            chooseFire();
            fireTimer = fireTime;
        }
    }

    private void chooseDirection()
    {
        int horizontal = Random.Range(-1, 1);
        int vertical = Random.Range(0, 1);
        charControl.SendDirInput(new Vector2(horizontal, vertical));
    }

    private void chooseStance()
    {
        int stance = Random.Range(0, 5);
        if(stance <= 2)
        {
            charControl.Stand();
        }
        else if(stance <= 4)
        {
            charControl.Crouch();
        }
        else if(stance <= 5)
        {
            charControl.Prone();
        }
    }

    private void chooseFire()
    {
        int willFire = Random.Range(0, 3);
        if(willFire >= 2)
        {
            charControl.Fire();
        }
    }
}
