using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject cardPrefab;
    public Transform handContainer;
    public Transform deckOrigin; // The new "DeckPile" object
    public int maxHandSize = 8;
    
    [Header("Animation Settings")]
    public float flySpeed = 0.2f; // How fast the card flies (seconds)
    public float timeBetweenCards = 0.1f; // Delay between dealing each card

    [Header("Data")]
    public List<Sprite> allCardImages = new List<Sprite>();
    
    private List<Sprite> deck = new List<Sprite>();
    private List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        // Clear old cards
        foreach (GameObject card in cardsInHand)
        {
            if(card != null) Destroy(card);
        }
        cardsInHand.Clear();

        // Setup Deck
        deck = new List<Sprite>(allCardImages);
        ShuffleDeck();

        // START THE ANIMATED DEALING
        StartCoroutine(DealHandRoutine());
    }

    // This special function can "wait" over time
    IEnumerator DealHandRoutine()
    {
        while (cardsInHand.Count < maxHandSize && deck.Count > 0)
        {
            DrawOneCard();
            // Wait a split second before dealing the next one
            yield return new WaitForSeconds(timeBetweenCards);
        }
    }

    void DrawOneCard()
    {
        // 1. Spawn at the DECK position (Parent is the Canvas/Root, not the Hand yet!)
        // We use handContainer.parent to ensure it's inside the Canvas but floating free
        GameObject newCard = Instantiate(cardPrefab, deckOrigin.position, Quaternion.identity, handContainer.parent);

        // 2. Set Image and Name
        Sprite cardSprite = deck[0];
        newCard.GetComponent<Image>().sprite = cardSprite;
        newCard.name = cardSprite.name;

        // 3. Add to list
        cardsInHand.Add(newCard);
        deck.RemoveAt(0);

        // 4. Start the flight animation
        StartCoroutine(FlyCardToHand(newCard));
    }

    IEnumerator FlyCardToHand(GameObject card)
    {
        float timer = 0;
        Vector3 startPos = card.transform.position;
        
        // We want to fly towards the Hand Container
        // Note: This is an approximation. It flies to the center of the hand.
        Vector3 endPos = handContainer.position; 

        while (timer < flySpeed)
        {
            // Move the card smoothly
            timer += Time.deltaTime;
            card.transform.position = Vector3.Lerp(startPos, endPos, timer / flySpeed);
            
            // Wait for next frame
            yield return null; 
        }

        // 5. THE LANDING
        // Now that we are close, we snap it into the Hand Container
        if (card != null)
        {
            card.transform.SetParent(handContainer);
            
            // Reset scale (sometimes parenting changes scale)
            card.transform.localScale = Vector3.one; 
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Sprite temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}