using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform PlatformGenerator;
    private Vector3 PlatformStartPoint;
    public PlayerController thePlayer;
    private Vector3 PlayerStartPoint;
    private PlatformDestruction[] platformList;
    private ScoreManager theScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        PlatformStartPoint = PlatformGenerator.position;
        PlayerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame() {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo() {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        platformList = FindObjectsOfType<PlatformDestruction>();

        for(int i=0;i < platformList.Length; i++) {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = PlayerStartPoint;
        PlatformGenerator.position = PlatformStartPoint;
        thePlayer.gameObject.SetActive(true);
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }
}
