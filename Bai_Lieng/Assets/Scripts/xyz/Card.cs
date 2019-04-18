using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public enum CardType
{
	Diamond, 
	Spade,
	Heart,
	Club
}

public class Card : MonoBehaviour
{
    GameManager_Cards gameManager;

	public int cardID;
	public int cardNumber;
	public CardType cardType;
    public Sprite cardSprite;

    public Sprite defaultSprite;

	//AllCards_Detail allCards;
	Random_ButThat randomCards;

    public float flipBackTime = 0.5f;

    public bool hasBeenChacked = false;

	void Start()
	{
        //allCards = FindObjectOfType<AllCards_Detail>();
        //		randomCards = FindObjectOfType<Random_ButThat> ();

        gameManager = FindObjectOfType<GameManager_Cards>();
	}

	public void InitCard(int id)
	{
        //string cardName = "card" + id;


        if (AllCards_Detail.instance.cards != null)
        {
            cardID = AllCards_Detail.instance.cards[id].cardId;
            cardNumber = AllCards_Detail.instance.cards[id].cardNumber;
            cardType = AllCards_Detail.instance.cards[id].cardType;

            cardSprite = AllCards_Detail.instance.cards[id].cardSprite;
        }
        else
        {
            print("Something bad happened! Please Reset the Game");
        }
            

        GetComponent<SpriteRenderer>().sprite = defaultSprite;

        //		cardID = randomCards.cardIds [5];
    }

    public void FlipCard_Face()
    {
        GetComponent<SpriteRenderer>().sprite = cardSprite;
        gameObject.transform.parent.parent.GetComponent<PlayerController>().CardsOnFace += 1;

        if (!hasBeenChacked)
        {
            gameManager.checkedNumberOfCards += 1;
        }
        hasBeenChacked = true;

        

        Invoke("FlipCard_Back", flipBackTime);
    }

    void FlipCard_Back()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.transform.parent.parent.GetComponent<PlayerController>().CardsOnFace -= 1;
    }
}


