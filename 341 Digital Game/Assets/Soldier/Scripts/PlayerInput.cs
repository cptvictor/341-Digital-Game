using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterControls controlActions;

    public GameObject characterCamera;

    [SerializeField]
    private float lookSpeed = 2.0f;

    private float lookYInput;

    private float lookXInput;

    // Start is called before the first frame update
    void Start()
    {
        controlActions = GetComponent<CharacterControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controlActions.SendDirInput(moveInput);

        lookYInput += Input.GetAxis("Mouse X") * lookSpeed;
        lookXInput += Input.GetAxis("Mouse Y") * lookSpeed;
        lookXInput = Mathf.Clamp(lookXInput, -90f, 90f);

        characterCamera.transform.localRotation = Quaternion.Euler(-lookXInput, 0f, 0f);
        controlActions.RotateChar(lookYInput);


        int curStance = controlActions.GetStance();
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (curStance == 1)
                controlActions.Stand();
            else
                controlActions.Crouch();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (curStance == 0)
                controlActions.Crouch();
            else
                controlActions.Prone();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curStance == 2)
                controlActions.Jump();
            else
            {
                controlActions.Stand();
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            controlActions.StopJump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controlActions.changeRunState(true);
            if (curStance != 2)
                controlActions.Stand();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            controlActions.changeRunState(false);
        }
        if(Input.GetMouseButtonDown(0))
        {
            controlActions.Fire();
        }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     GameManager.Instance().chooseNewSoldier(this.gameObject);
        // }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public GameObject GetCamera()
    {
        return characterCamera;
    }
}
