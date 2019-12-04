using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject[] soldierList;

    [TextArea(2, 10000)]
    public string[] letterList;

    private int letterIndex;

    public GameObject managerCam;

    private Text letterText;

    public float deathCamTime = 5f;

    private float deathCamTimer;

    private bool onDeathScreen;

    private GameObject recentDeath;

    public Transform[] spawnPointList;

    public float waveTime = 3f;

    private float waveTimer;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        letterText = managerCam.GetComponentInChildren<Text>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        waveTimer = waveTime;
        letterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (onDeathScreen)
        {
            deathCamTimer -= Time.deltaTime;
            if (deathCamTimer <= 0)
            {
                managerCam.SetActive(false);
                chooseNewSoldier(recentDeath);
                onDeathScreen = false;
            }
        }

        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            respawnSoldiers();
            waveTimer = waveTime;
        }
    }

    public static GameManager Instance()
    {
        return instance;
    }

    public void processDeath(GameObject deadSoldier)
    {
        recentDeath = deadSoldier;
        deathCamTimer = deathCamTime;

        letterText.text = letterList[letterIndex++];
        if (letterIndex >= letterList.Length)
            letterIndex = 0;

        managerCam.SetActive(true);
        onDeathScreen = true;
    }

    private void chooseNewSoldier(GameObject oldSoldier)
    {
        int randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length));
        GameObject newSoldier = soldierList[randSoldier];
        while (newSoldier.GetInstanceID() == oldSoldier.GetInstanceID() && newSoldier.activeSelf == false)
        {
            randSoldier = Mathf.RoundToInt(Random.Range(0, soldierList.Length));
            newSoldier = soldierList[randSoldier];
        }

        newSoldier.GetComponent<AiInput>().enabled = false;
        PlayerInput newPlayer = newSoldier.GetComponent<PlayerInput>();
        newPlayer.enabled = true;
        newPlayer.GetCamera().SetActive(true);
    }

    private void respawnSoldiers()
    {
        for (int i = 0; i < soldierList.Length; i++)
        {
            GameObject curSoldier = soldierList[i];
            if (curSoldier.activeSelf == false)
            {
                curSoldier.transform.position = spawnPointList[i].position;
                curSoldier.transform.rotation = spawnPointList[i].rotation;
                curSoldier.SetActive(true);
            }
        }
    }

}
