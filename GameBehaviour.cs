using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

// KETERANGAN:
// kelas ini dapat direpresentasikan sebagai juri permainan
// 1. menyiapkan arena (posisi player) 	-> initPlayers()
// 2. menyiapkan deck					-> initDeck()
// 3. membagikan 11 kartu kepada setiap pemain saat awal permainan -> spreadCardsToAllPlayers(maxCardsEachPlayerStart);
// 4. TODO: menentukan player yg active (giliran)
public interface Itest
{
    void test();
}
public class GameBehaviour : MonoBehaviour
{

    //di arena ada pemain(player) dan tumpukan kartu (deck)
    public int playerCount = 4;
    public int maxCardsEachPlayer = 12;
    public int maxCardsEachPlayerStart = 11;
    public GameObject player;
    public GameObject deck;
    GameObject[] players;
    GameObject activePlayer;
    int activePlayerNum;    
    // Use this for initialization

    void Awake()
    {
        Player.OnFinishPlayerTurn += activateNextPlayer;
        Player.onOtherTrashTaken += onPlayerRequestTrash;
    }

    void Start()
    {
        Debug.Log("GameBehaviour Start");
        initDeck();
        initPlayers();
        spreadCardsToAllPlayers(maxCardsEachPlayerStart); // bagi-bagi kartu
        generateFirstPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Kresna - Initiate Deck in Arena - START
    void initDeck()
    {
        this.deck = Instantiate(this.deck);
        Deck scriptDeck = (Deck)this.deck.GetComponent(typeof(Deck));
        Debug.Log("Cards in Deck: " + scriptDeck.countCards());
    }
    //Kresna - Initiate Deck in Arena - END

    //Kresna - Initiate Players in Arena - START
    void initPlayers()
    {
        this.players = new GameObject[this.playerCount];
        for (int i = 0; i < this.playerCount; i++)
        {
            this.players[i] = Instantiate(this.player);
            Player scriptPlayer = (Player)this.players[i].GetComponent(typeof(Player));
            scriptPlayer.setNoUrut(i);
        }
    }
    //Kresna: Initiate Players in Arena - END

    //Kresna - memindah kartu - START 
    //dilakukan oleh host(wasit/pembagi kartu), awal main
    bool moveCard(GameObject deck, GameObject targetPlayer)
    {
        Deck scriptDeck = (Deck)deck.GetComponent(typeof(Deck));
        if (scriptDeck.countCards() < 1)
        {
            Debug.Log("Kartu di Deck telah habis!");
            return false; // kartu habis
        }
        Card.Card card = scriptDeck.getRandomCard();
        if (!scriptDeck.removeCard(card))
        {
            //	Debug.Log ("Cannot remove card!");
            return false; //error remove card
        }

        Player scriptPlayer = (Player)targetPlayer.GetComponent(typeof(Player));
        scriptPlayer.addCardToHand(card);
        //Debug.Log ("moveCard("+ card.code +")");
        return true; //berhasil memindah kartu
    }
    //Kresna: memindah kartu - END

    //Kresna - Bagi bagi kartu ke semua player - START
    //dilakukan oleh host (wasit/pembagi kartu), awal main
    void spreadCardsToAllPlayers(int cardsCountEachPlayer)
    {
        for (int playerIter = 0; playerIter < this.playerCount; playerIter++)
        {
            for (int i = 0; i < cardsCountEachPlayer; i++)
            {
                moveCard(this.deck, this.players[playerIter]);
            }
        }
        Deck scriptDeck = (Deck)this.deck.GetComponent(typeof(Deck));
        Debug.Log("Cards in deck after spreadCardsToAllPlayers(" + cardsCountEachPlayer + "): " + scriptDeck.countCards());
    }
    //Kresna - Bagi bagi kartu ke semua player - END

    //Kresna - Set active current player - START
    void generateFirstPlayer()
    {
        int randNum = Random.Range(0, playerCount); // Random.Range( 0, 10 ) will return values between 0 and 9
        activePlayer = players[randNum];
        int activePlayerNum = randNum;
        Player scriptPlayer = (Player)activePlayer.GetComponent(typeof(Player));
        scriptPlayer.activate();
        Debug.Log("First Player: " + scriptPlayer.isActive + ", " + scriptPlayer.noUrut);
    }
    //Kresna - Set active current player - END

    void activateNextPlayer()
    {
        if (activePlayerNum >= playerCount - 1)
        {
            activePlayerNum = 0;
            activePlayer = players[0];
        }
        else
        {
            activePlayerNum++;
            activePlayer = players[activePlayerNum];
        }
        Player scriptPlayer = (Player)activePlayer.GetComponent(typeof(Player));
        scriptPlayer.activate();
        Deck scriptDeck = (Deck)this.deck.GetComponent(typeof(Deck));
        if (scriptDeck.countCards() <= 0)
        {
            Debug.Log("Game End -> Deck Empty.");
        }
        Debug.Log("Deck cards: " + scriptDeck.countCards());
        //Debug.Log ("next player["+ activePlayerNum +"] active");
    }

    public Card.Card onPlayerRequestTrash()
    {
        int prevPlayerNum = activePlayerNum - 1;
        if (prevPlayerNum < 0)
        {
            prevPlayerNum = playerCount - 1;
        }
        Player scriptPlayer = (Player)players[prevPlayerNum].GetComponent(typeof(Player));
        return scriptPlayer.onTrashTakenByOtherPlayer();
    }
}
