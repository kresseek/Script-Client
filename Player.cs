using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card;

enum PlayerState{
	nonActive = 0,
	activeGetCard = 1,
	activeDropCard = 2
}

public class Player : MonoBehaviour{

	public GameObject MyHand; // #kresna -> pengganti Gameobject.find("Hand") karena berat
    public GameObject MyTrash;
	int maxCards = 12;
	public int noUrut { get; set; }
	List<Card.Card> cardsInHand;
	public List<Card.Card> cardsInTrash { get; set; }
	public bool isActive { get; set; }
	PlayerState currentState;

	public delegate Card.Card GetOneCardFromDeck ();
	public static event GetOneCardFromDeck onDeckTaken;
	public delegate void FinishTurn ();
	public static event FinishTurn OnFinishPlayerTurn;
	public delegate Card.Card GetOneCardFromTrash ();
	public static event GetOneCardFromTrash onOtherTrashTaken;


	void Awake(){
        tarikGan.onClick.AddListener(() => buttoncallback());
        cardsInHand = new List<Card.Card> ();
		cardsInTrash = new List<Card.Card> ();
		isActive = false;
	}

    void buttoncallback()
    {
        tarikgan();
    }
	void Start () {
	
        //		Itest itest = (Itest) GetComponent (typeof(Itest));
		//		itest.test ();
	}

	// Update is called once per frame
	void Update () {
		if (isActive && currentState != PlayerState.nonActive) {
            if (Input.GetKeyDown("space"))
                tarikgan();
            else if (Input.GetKeyDown("a"))
            {
                if (currentState == PlayerState.activeGetCard)
                {
                    TakeFromTrash();
                    return;
                }
                else
                {
                    Debug.Log("Player " + noUrut + " Space");
                }
            }


		}
	}

    public Button tarikGan;



    public void tarikgan()
    {
				if (currentState == PlayerState.activeDropCard)
				{
					sendCardToTrash(selectCardInHandRandom());
}
				else if (currentState == PlayerState.activeGetCard)
				{
					getOneCardFromDeck();
					return;
				}
				else
				{
					Debug.Log("Player " + noUrut + " Space");
				}
			}

	public void setNoUrut(int noUrut){
		this.noUrut = noUrut;
	}

	public bool getOneCardFromDeck(){
		if (cardsInHand.Count < maxCards) { // kartu yg boleh dipegang maksimal 12 kartu (player mengambil sendiri kartu dari deck)
			cardsInHand.Add (onDeckTaken());
			currentState = PlayerState.activeDropCard;

			Debug.Log ("Player" + noUrut + " getOneCardFromDeck, Total cards: " + cardsInHand.Count);
			printCardsInHand ();
			printCardsInTrash ();
			return true;
		}
		Debug.Log ("Player" + noUrut + " reach maximum of cards in hand(" + maxCards + ")");
		return false;
	}

	public bool addCardToHand(Card.Card card){
		if (cardsInHand.Count < maxCards - 1) { //pengaman. kartu yg boleh dipegang di awal permainan hanya 11 kartu. (player mendapatkan kartu dari host/wasit pertandingan)
			cardsInHand.Add (card);
			return true;
		}
		Debug.Log ("Player" + noUrut + " reach maximum of cards in hand(" + maxCards + ")");
		return false;
	}

	public void sendCardToTrash(Card.Card card){
		cardsInTrash.Add (card);
		cardsInHand.Remove (card);
		//Debug.Log("Player" + noUrut + " sendCardToTrash");
		printCardsInHand();
		printCardsInTrash ();
		deactivate (); // also deactivate player
	}

	public Card.Card selectCardInHandRandom(){
		//Temp-implement random card selected from hand
		int x = Random.Range(0, maxCards - 1);
		return cardsInHand [x];
	}
	public Card.Card selectCardInHand(){
		//TODO : user can select card
		return null;
	}
	// melihat kartu yang ada di trash semua player
	public void seeOtherTrash(){

	}
	// mengambil kartu dari player's trash sebelah kiri
	public void TakeFromTrash(){
		onOtherTrashTaken ();
	}

	public void activate(){
		this.isActive = true;
		this.currentState = PlayerState.activeGetCard;
		Debug.Log ("Player"+noUrut+" active");
	}
	public void deactivate(){
		this.isActive = false;
		this.currentState = PlayerState.nonActive;
		StartCoroutine(ExecuteAfterTime(0.5f));
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		// Code to execute after the delay
		OnFinishPlayerTurn ();
	}

	public Card.Card onTrashTakenByOtherPlayer(){
		Card.Card topTrash = getTopTrash ();
		removeTopTrash ();
		return topTrash;
	}

	private Card.Card getTopTrash(){
		if (cardsInTrash.Count <= 0) {
			return null;
		}
		Card.Card topTrashCard = cardsInTrash [cardsInTrash.Count - 1];
		return topTrashCard;
	}

	private bool removeTopTrash(){
		if (cardsInTrash.Count <= 0) {
			return false;
		}
		cardsInTrash.Remove (cardsInTrash[cardsInTrash.Count-1]);
		return true;
	}

	private void printCardsInHand(){
		string toPrint = "";
		// #kresna -> hapus card yang ada di hand
		foreach (Transform card in MyHand.transform)
		{
			Destroy(card.gameObject);
		}
		// #kresna -> tampilkan cards
		foreach (Card.Card card in cardsInHand) {
			GameObject cardObj = Instantiate(Resources.Load("CardHolder")) as GameObject;
			cardObj.transform.SetParent(MyHand.transform); // #kresna -> ganti biar ga pake find soalnya fungsi find() lebih berat (membandingkan string itu berat)
			cardObj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + card.negara + "_" + (int)((int)card.gambarDalam + 1)); //#kresna -> contoh hanya hiu_1
			cardObj.name = card.negara + "_" + (int)((int)card.gambarDalam + 1) + "_" +card.code;
			Debug.Log(card.negara + "_" + (int)card.gambarDalam);
			cardObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		Debug.Log ("Player"+noUrut+" Cards in Hand: "+toPrint);
	}
	private void printCardsInTrash(){
        string toPrint = "";
        // #kresna -> hapus card yang ada di hand
        foreach (Transform card in MyTrash.transform)
        {
            Destroy(card.gameObject);
        }
        // #kresna -> tampilkan cards
        foreach (Card.Card card in cardsInTrash)
        {
            GameObject cardObj = Instantiate(Resources.Load("CardBin")) as GameObject;
            cardObj.transform.SetParent(MyTrash.transform); // #kresna -> ganti biar ga pake find soalnya fungsi find() lebih berat (membandingkan string itu berat)
            cardObj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + card.negara + "_" + (int)((int)card.gambarDalam + 1)); //#kresna -> contoh hanya hiu_1
            cardObj.name = card.negara + "_" + (int)((int)card.gambarDalam + 1) + "_" + card.code;
            Debug.Log(card.negara + "_" + (int)card.gambarDalam);
            cardObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        Debug.Log("Player" + noUrut + " Cards in Hand: " + toPrint);

    }
    public virtual void OnTurnStart()
	{
		// add one mana crystal to the pool;
		Debug.Log("In ONTURNSTART for " + gameObject.name);

	}
}
