using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Card Info")]
    public string cardName;    // e.g., "Ace of Spades"
    public int rank;           // 1 (Ace), 2, 3... 11 (Jack), 12 (Queen), 13 (King)
    public int scoreValue;     // e.g., 11 for Ace, 10 for Face cards in Blackjack
    
    // An "enum" is a dropdown list for easy selecting
    public Suit cardSuit;      
    
    [Header("Visual")]
    public Sprite cardImage;   // The image we used before
}

// Define the Suits outside the class
public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}