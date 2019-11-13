using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// general actions the characters can take
/// </summary>
public class CharacterControls : MonoBehaviour
{
    private CharacterController charControl;

    [SerializeField]
    private GameObject charCamera;

    /// <summary>
    /// speed when walking normally
    /// </summary>
    [SerializeField]
    private float walkSpeed = 3f;

    /// <summary>
    /// speed when sprinting
    /// </summary>
    [SerializeField]
    private float runSpeed = 5f;

    /// <summary>
    /// speed when crouched
    /// </summary>
    [SerializeField]
    private float crouchSpeed = 1f;

    /// <summary>
    /// speed when crawling on the ground
    /// </summary>
    [SerializeField]
    private float proneSpeed = 0.3f;

    [SerializeField]
    private float jumpSpeed = 40f;

    [SerializeField]
    private float gravity = 100f;

    /// <summary>
    /// what stance the character is in
    /// 0 - prone
    /// 1 - crouching
    /// 2 - standing
    /// </summary>
    private int stance;

    /// <summary>
    /// is character sprinting
    /// </summary>
    private bool isRunning;

    private bool isJumping;

    private Vector3 desiredDir;

    // Start is called before the first frame update
    void Start()
    {
        charControl = GetComponent<CharacterController>();
        stance = 2;
    }

    /// <summary>
    /// change stance to crouch
    /// </summary>
    public void Crouch()
    {
        stance = 1;
        charCamera.transform.localPosition = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// change stance to prone
    /// </summary>
    public void Prone()
    {
        stance = 0;
        charCamera.transform.localPosition = new Vector3(0, -0.8f, 0);
    }

    /// <summary>
    /// change stance to standing
    /// </summary>
    public void Stand()
    {
        stance = 2;
        charCamera.transform.localPosition = new Vector3(0, 0.8f, 0);
    }

    public int GetStance()
    {
        return stance;
    }

    /// <summary>
    /// change running state
    /// </summary>
    /// <param name="isSprint"></param>
    public void changeRunState(bool isSprint)
    {
        isRunning = isSprint;
    }

    public void Jump()
    {
        isJumping = true;
    }

    public void StopJump()
    {
        isJumping = false;
    }

    /// <summary>
    /// move the character
    /// </summary>
    /// <param name="inputDir">the direction to move in</param>
    public void MoveChar(Vector2 inputDir)
    {
        if (charControl.isGrounded)
        {
            desiredDir = transform.forward * inputDir.y + transform.right * inputDir.x;

            float moveMult;
            switch (stance)
            {
                case 0:
                    moveMult = proneSpeed;
                    break;
                case 1:
                    moveMult = crouchSpeed;
                    break;
                default:
                    if (isRunning)
                        moveMult = runSpeed;
                    else
                        moveMult = walkSpeed;
                    break;
            }
            desiredDir *= moveMult;

            if (isJumping)
            {
                desiredDir.y = jumpSpeed;
                isJumping = false;
            }
        }
        desiredDir.y -= gravity * Time.deltaTime;

        charControl.Move(desiredDir * Time.deltaTime);
    }

    /// <summary>
    /// rotate the character
    /// </summary>
    /// <param name="inputDir">direction to rotate</param>
    public void RotateChar(float rotAmount)
    {
        transform.Rotate(0, rotAmount, 0);
    }
}
