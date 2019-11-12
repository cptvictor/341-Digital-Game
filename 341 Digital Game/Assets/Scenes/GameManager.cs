using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject[] soldierList;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager Instance()
    {
        return instance;
    }

    public void chooseNewSoldier(GameObject oldSoldier)
    {
        // int randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length-1));
        // GameObject newSoldier = soldierList[randSoldier];
        // Debug.Log(randSoldier);
        // while(newSoldier.GetInstanceID() == oldSoldier.GetInstanceID())
        // {
        //     randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length-1));
        //     newSoldier = soldierList[randSoldier];
        // }

        PlayerInput newPlayer = newSoldier.GetComponent<PlayerInput>();
        newPlayer.enabled = true;
        newPlayer.GetCamera().SetActive(true);

        PlayerInput oldPlayer = oldSoldier.GetComponent<PlayerInput>();
        oldPlayer.enabled = false;
        oldPlayer.GetCamera().SetActive(false);
    }
}
