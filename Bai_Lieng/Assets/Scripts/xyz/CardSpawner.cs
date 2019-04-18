using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour 
{
	public Card cardPrefab;

	Random_ButThat randomCards;
	//AllCards_Detail allCards;

    public Button spawnButton;
    public Button distributeCardsButton;
    public Text selectUsersText;

    public GameObject playersParent;

    void Start()
	{
		randomCards = FindObjectOfType<Random_ButThat> ();
        //allCards = FindObjectOfType<AllCards_Detail> ();

        //spawnButton.onClick.AddListener(()=>
        //{
        //    FindObjectOfType<Random_ButThat>().CreateCardsArray();
        //});
        spawnButton.onClick.AddListener(SpawnNeededCards);
    }


	public void SpawnNeededCards()
	{
		for (int i = 0; i < randomCards.cardIds.Length; i++) 
		{
			SpawnCard (randomCards.cardIds[i]);
		}
        //		Card[] cards_ = GetComponentsInChildren<Card> ();

        spawnButton.gameObject.SetActive(false);
        distributeCardsButton.gameObject.SetActive(true);
        selectUsersText.gameObject.SetActive(false);

    }

	void SpawnCard(int i)
	{
		Card cardObj = Instantiate (cardPrefab) as Card;
		cardObj.transform.SetParent (transform, false);

        cardObj.InitCard(i);
    }
}
