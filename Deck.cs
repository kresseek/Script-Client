using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

public class Deck : MonoBehaviour {
	List<Card.Card> cards { get; set; }

	void Awake(){
		Debug.Log ("Deck is awake");
		Player.onDeckTaken += OnCardTaken;
		initCards ();
	}

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		
	}

	void OnDisable(){
		Player.onDeckTaken -= OnCardTaken;
	}

	//#Kresna: Initiate Cards - START
	void initCards(){
		cards = new List<Card.Card>();
		//create hiu
		int iterasi = 0;
		for(int i=0; i<6; i++){
			for(int j=0; j<6; j++){
				cards.Add(new Card.Card (iterasi, (Negara)0, (GambarDalam)i, j));
				iterasi++;
			}
            Debug.Log(cards);
			//Debug.Log ("Create card Hiu - Gambar Dalam "+ i +" has done");
		}

		//create negara lain
		for(int i=1; i<9; i++){
			for(int j=0; j<3; j++){
				for(int k=0; k<6; k++){
					cards.Add(new Card.Card (iterasi, (Negara)i, (GambarDalam)j, k));
					iterasi++;
				}
			}
			//Debug.Log ("Create card Negara "+ i +" has done");
		}
		Debug.Log ("Create cards has done");
	}
	//#Kresna: Initiate Cards - END

	public Card.Card getRandomCard(){
		int randIndex = Random.Range (0, this.cards.Count - 1);
		return this.cards [randIndex];
	}

	public bool removeCard(Card.Card card){
		return this.cards.Remove (card);
	}

	public int countCards(){
		return cards.Count;
	}

	public Card.Card OnCardTaken(){
		Card.Card cardTaken = getRandomCard ();
		cards.Remove (cardTaken);
		//Debug.Log ("OnCardTaken by Deck.cs");
		return cardTaken;
	}

}
