using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool canTakeCards = true;
    public Color[] toggleColors;

    public SpriteRenderer toggleImage;
    public Transform cardSpawnPosTransform;

    [SerializeField]
    GameObject winnerPanel;

    [HideInInspector]
    public int i = 0;

    private int cardsOnFace = 0;
    public int CardsOnFace { get { return cardsOnFace; } set { cardsOnFace = value; } }

    bool hasCheckedWin = false;

    public void ToggleCanTakeCard()
    {
        i++;
        toggleImage.color = toggleColors[i % 2];

        canTakeCards = !canTakeCards;


    }

    void Update()
    {
        //		CheckWin ();
        //Card[] cards = cardSpawnPosTransform.GetComponentsInChildren<Card>();
        //foreach (Card card in cards)
        //{
        //    if (card.GetComponent<SpriteRenderer>().sprite != card.defaultSprite)
        //    {
        //        // do something increment something
        //        CardsOnFace++;
        //    }
        //}

        if (CardsOnFace == 3 && !hasCheckedWin)
        {
            CheckWin();
        }

    }

    public void CheckWin()
    {
        hasCheckedWin = true;
        if (cardSpawnPosTransform.childCount != 0)
        {
            Card[] cards = cardSpawnPosTransform.GetComponentsInChildren<Card>();
            if (cards[0].cardNumber == cards[1].cardNumber && cards[0].cardNumber == cards[2].cardNumber)
            {
                print("Player " + transform.name + " wins");

                ShowWinnerPanel();
            }
        }
        print("Checked for win!");
        
    }

    void ShowWinnerPanel()
    {
        winnerPanel.gameObject.SetActive(true);
        winnerPanel.GetComponentInChildren<Text>().text = transform.name + " Wins !";

        Time.timeScale = 0; //Pause the game to keep the winning cards up
    }

    // if all three cards have other than default sprite, check for Win()

}
