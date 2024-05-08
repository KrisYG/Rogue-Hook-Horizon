using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int unlockedLevel1 = 1;
    public int unlockedLevel2 = 0;
    public int unlockedLevel3 = 0;
    [SerializeField] GameObject doorOne;
    [SerializeField] GameObject doorTwo;
    [SerializeField] GameObject doorThree;
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;

    void Start()
    {
        SpriteRenderer doorOneSprite = doorOne.GetComponent<SpriteRenderer>();
        SpriteRenderer doorTwoSprite = doorTwo.GetComponent<SpriteRenderer>();
        SpriteRenderer doorThreeSprite = doorThree.GetComponent<SpriteRenderer>();
        PlayerPrefs.SetInt("Level_1", 1);
        unlockedLevel1 = PlayerPrefs.GetInt("Level_1");
        Debug.Log(PlayerPrefs.GetInt("Level_2"));
        unlockedLevel2 = PlayerPrefs.GetInt("Level_2");
        unlockedLevel3 = PlayerPrefs.GetInt("Level_3");

        // Open door one
        if (unlockedLevel1 == 1)
        {
            doorOneSprite.sprite = openDoorSprite;
            doorOne.GetComponent<Enter>().unlocked = true;
        }
        else
        {
            doorOneSprite.sprite = closedDoorSprite;
            doorOne.GetComponent<Enter>().unlocked = false;
        }

        // Open door two
        if (unlockedLevel2 == 1)
        {
            doorTwoSprite.sprite = openDoorSprite;
            doorTwo.GetComponent<Enter>().unlocked = true;
        }
        else
        {
            doorTwoSprite.sprite = closedDoorSprite;
            doorTwo.GetComponent<Enter>().unlocked = false;
        }

        // Open door three
        if (unlockedLevel3 == 1)
        {
            doorThreeSprite.sprite = openDoorSprite;
            doorThree.GetComponent<Enter>().unlocked = true;
        }
        else
        {
            doorThreeSprite.sprite = closedDoorSprite;
            doorThree.GetComponent<Enter>().unlocked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
