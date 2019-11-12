using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterControls controlActions;

    [SerializeField]
    private GameObject characterCamera;

    // Start is called before the first frame update
    void Start()
    {
        controlActions = GetComponent<CharacterControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controlActions.MoveChar(moveInput);

        Vector2 lookInput = new Vector2(Input.GetAxis("MouseX"), Input.GetAxis("MouseY"));


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controlActions.Crouch();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            controlActions.Prone();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controlActions.Stand();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controlActions.changeRunState(true);
            controlActions.Stand();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            controlActions.changeRunState(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameManager.Instance().chooseNewSoldier(this.gameObject);
        }
    }

    void OnEnable()
    {
        characterCamera.SetActive(true);
    }

    void OnDisable()
    {
        characterCamera.SetActive(false);
    }
}
