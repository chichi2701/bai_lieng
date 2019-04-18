using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

[System.Serializable]
public struct IndividualCard
{
	public int cardId;
	public int cardNumber;
	public CardType cardType;
    public Sprite cardSprite;
}


public class AllCards_Detail : MonoBehaviour 
{
	public IndividualCard[] cards;

    public static AllCards_Detail instance;

    public Sprite[] cardSprites;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            instance = this;
            Destroy(instance.gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        Thread initAllCardsThread = new Thread(new ThreadStart(InitIndividualCards));
        initAllCardsThread.Start();

    }


	void InitIndividualCards()
	{
		cards = new IndividualCard[52];
		
		for (int i = 0; i < cards.Length; i++) 
		{
			cards [i].cardId = i + 1;
			cards [i].cardNumber = (cards [i].cardId % 13 == 0) ? 13 : cards [i].cardId % 13;

            //print(cards[i].cardId / 13);

			cards [i].cardType = (CardType)(i / 13);

            cards[i].cardSprite = cardSprites[i];

            Thread.Sleep(20);


        }

    }
}
