using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributeCards : MonoBehaviour
{
    public Transform cardsParent;
    public Transform playersParent;

    public int cardsToDistribute = 3;

    public float offset = 0.2f;
    public float offsetLocal = 0.3f;

    GameManager_Cards gameManager;

    public float lerpRate = 0.4f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Cards>();
    }

    public void DistributeToAvailablePlayers()
    {
        PlayerController[] players = playersParent.GetComponentsInChildren<PlayerController>();

        foreach (PlayerController player in players)
        {
            if (player.canTakeCards)
            {
                // give card to player
                DistributeToPlayer(player);

            }
            player.GetComponent<BoxCollider2D>().enabled = false; //Dsable player canTakeCards toggling
        }

        //Delete remaining cards from Cards Parent
        Card[] remainingCards = cardsParent.GetComponentsInChildren<Card>();
        foreach (Card card in remainingCards)
            Destroy(card.gameObject);

        
        GetComponent<Button>().interactable = false;

        //gameObject.SetActive(false); // disable distribute button
    }

    void DistributeToPlayer(PlayerController player)
    {
        for (int i = 0; i < cardsToDistribute; i++)
        {
            JumpCardTo(player, i);
        }
    }


    void JumpCardTo(PlayerController player, int cardIndex) //new
    {
        Transform cardT = cardsParent.GetChild(0).transform;
        Transform playerCardPosT = player.transform.GetChild(0).transform;

        cardT.SetParent(playerCardPosT);

        //Vector3 newPos = Vector3.zero;
        Vector3 newPos_local = Vector3.zero;

        switch (cardIndex)
        {
            case 0:
                //newPos = playerCardPosT.position;
                newPos_local = Vector3.zero;
                break;
            case 1:
                //newPos = new Vector3(playerCardPosT.position.x - offset, playerCardPosT.position.y, playerCardPosT.position.z);
                newPos_local = new Vector3(newPos_local.x - offsetLocal, newPos_local.y, newPos_local.z);
                break;
            case 2:
                //newPos = new Vector3(playerCardPosT.position.x + offset, playerCardPosT.position.y, playerCardPosT.position.z);
                newPos_local = new Vector3(newPos_local.x + offsetLocal, newPos_local.y, newPos_local.z);
                break;
        }

        cardT.localRotation = Quaternion.Euler(Vector3.zero);
        //cardT.position = Vector3.Lerp(cardT.position, newPos, lerpRate);


        //cardT.localPosition = Vector3.zero;
        StartCoroutine(AnimateCardToPlayer_Local(cardT, newPos_local));

        
    }


    IEnumerator AnimateCardToPlayer_Local(Transform card, Vector3 newPos)
    {
        float timeElapsed = 0;
        float timeToLerp = 1f;

        while (timeElapsed < timeToLerp)
        {
            card.localPosition = Vector3.Lerp(card.localPosition, newPos, lerpRate); //old
            //card.position = Vector3.Lerp(card.localPosition, newPos, lerpRate); //new

            if (Vector3.Distance(card.position, newPos) < 0.01f)
            {
                card.localPosition = newPos;
                timeElapsed = timeToLerp + 1;
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        gameManager.allAvailableCards_Number = FindObjectsOfType<Card>().Length;
        print("Total Cards : " + gameManager.allAvailableCards_Number);

        gameObject.SetActive(false); // disable distribute button
    }
}
