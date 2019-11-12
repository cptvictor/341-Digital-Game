using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private GameObject[] soldierList;

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
        oldSoldier.GetComponent<PlayerInput>().enabled = false;

        int randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length-1));
        GameObject newSoldier = soldierList[randSoldier];
        while(newSoldier == oldSoldier)
        {
            randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length-1));
            newSoldier = soldierList[randSoldier];
        }

        newSoldier.GetComponent<PlayerInput>().enabled = true;
    }
}
