using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    private GameObject[] deck;
    private int turn;
    public GameObject energy;
    private int playerHealth;
    public Slider playerhealthBar;
    public TMP_Text playerhealthText;
    private int enemyHealth;
    public Slider enemyhealthBar;
    public TMP_Text enemyhealthText;

    private bool playerTurn;
    private LayoutElement layoutElement;

    public GameObject playButton;
    public GameObject endTurnButton;
    private int energynum;

    private bool playerImmune;
    private bool enemyImmune;

    // Start is called before the first frame update
    void Start()
    {
        turn = 1;
        playerTurn = true;

        playerHealth = 400;
        enemyHealth = 400;
        //playerhealthBar = playerhealthBar.GetComponent<Slider>();
        playerhealthBar.maxValue = playerHealth;
        playerhealthBar.value = playerHealth;
        playerhealthText.text = playerHealth.ToString();
        //enemyhealthBar = playerhealthBar.GetComponent<Slider>();
        enemyhealthBar.maxValue = enemyHealth;
        enemyhealthBar.value = enemyHealth;
        enemyhealthText.text = enemyHealth.ToString();

        playerImmune = false;
        enemyImmune = false;

        deck = GameObject.FindGameObjectsWithTag("DeckCard");
        ShuffleDeck();
        int count = 0;
        GameObject[] movetoHand = new GameObject[5];
        TMP_Text energyText = energy.GetComponent<TMP_Text>();

        //switch case
        switch (turn) {
            case 1:
                energynum = 1;
                energyText.text = "1";
                break;
            case 2:
                energynum = 2;
                energyText.text = "2";
                break;
            case 3:
                energynum = 3;
                energyText.text = "3";
                break;
            case 4:
                energynum = 4;
                energyText.text = "4";
                break;
            case 5:
                energynum = 5;
                energyText.text = "5";
                break;
            case 6:
                energynum = 6;
                energyText.text = "6";
                break;
            case 7:
                energynum = 7;
                energyText.text = "7";
                break;
            case 8:
                energynum = 8;
                energyText.text = "8";
                break;
            case 9:
                energynum = 9;
                energyText.text = "9";
                break;
            default:    
                energynum = 9;
                energyText.text = "9";
                break;
        }
        foreach (GameObject card in deck) {
            if (count == 5) {
                break;
            }
            movetoHand[count] = card;
            count++;
        }
        foreach (GameObject card in movetoHand) {
            card.transform.SetParent(GameObject.Find("Hand").transform);
            card.transform.position = new Vector3(1, 0, 0);
            card.transform.tag = "HandCard";
            card.GetComponent<LayoutElement>().ignoreLayout = false;
        }
    }
    
    void ShuffleDeck() {
        for (int i = 0; i < deck.Length; i++) {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        return;
    }
    public void PlayCard() {
        GameObject[] cardsonField = GameObject.FindGameObjectsWithTag("FieldCard");
        if (cardsonField.Length == 1 && cardsonField[0].GetComponent<Card>().getCost() <= energynum) {
            energynum -= cardsonField[0].GetComponent<Card>().getCost();
            TMP_Text energyText = energy.GetComponent<TMP_Text>();
            energyText.text = energynum.ToString();
            int num = 0;
            switch(cardsonField[0].GetComponent<Card>().getID()) {
                case 1:
                    if (!enemyImmune) {
                        //generate random number between 1 and 10 inclusive
                        num = Random.Range(1, 11);
                        if (num == 1) {
                            enemyHealth -= 20;
                        }
                        enemyHealth -= 10;
                    }
                    break;
                case 2:
                    if (!enemyImmune) {
                        num = Random.Range(1, 11);
                        if (num == 1) {
                            enemyHealth -= 40;
                        }
                        enemyHealth -= 20;
                    }
                    break;
                case 3:
                    if (!enemyImmune) {
                        num = Random.Range(1, 11);
                        if (num == 1) {
                            enemyHealth -= 60;
                        }
                        enemyHealth -= 30;
                    }
                    break;
                case 4:
                    if (!playerImmune)
                        playerHealth = (int) playerHealth/2;
                    if (!enemyImmune) {
                        num = Random.Range(1, 11);
                        if (num == 1) {
                            enemyHealth -= 20;
                        }
                        enemyHealth -= 50;
                    }
                    break;
                case 5:
                    playerHealth += 10;
                    break;
                case 6:
                    //discard random card from hand
                    GameObject[] cardsonHand = GameObject.FindGameObjectsWithTag("HandCard");
                    int randomIndex = Random.Range(0, cardsonHand.Length);
                    cardsonHand[randomIndex].transform.position = new Vector3(1, 0, 0);
                    cardsonHand[randomIndex].transform.SetParent(GameObject.Find("Trash").transform);
                    cardsonHand[randomIndex].transform.tag = "TrashCard";
                    cardsonHand[randomIndex].GetComponent<LayoutElement>().ignoreLayout = false;

                    playerHealth += 10;
                    break;
                case 7: 
                    playerImmune = true;
                    break;
                case 8:
                    //draw 2 cards
                    GameObject[] movetoHand = new GameObject[2];
                    int count = 0;
                    foreach (GameObject card in deck) {
                        if (count == 2) {
                            break;
                        }
                        movetoHand[count] = card;
                        count++;
                    }
                    foreach (GameObject card in movetoHand) {
                        card.transform.SetParent(GameObject.Find("Hand").transform);
                        card.transform.position = new Vector3(1, 0, 0);
                        card.transform.tag = "HandCard";
                        card.GetComponent<LayoutElement>().ignoreLayout = false;
                    }
                    break;
                case 9:
                    //discard random card from hand and draw 2 cards
                    GameObject[] cardsonHand2 = GameObject.FindGameObjectsWithTag("HandCard");
                    int randomIndex2 = Random.Range(0, cardsonHand2.Length);
                    cardsonHand2[randomIndex2].transform.position = new Vector3(1, 0, 0);
                    cardsonHand2[randomIndex2].transform.SetParent(GameObject.Find("Trash").transform);
                    cardsonHand2[randomIndex2].transform.tag = "TrashCard";
                    cardsonHand2[randomIndex2].GetComponent<LayoutElement>().ignoreLayout = false;
                    //draw 2 cards
                    GameObject[] movetoHand2 = new GameObject[2];
                    int count2 = 0;
                    foreach (GameObject card in deck) {
                        if (count2 == 2) {
                            break;
                        }
                        movetoHand2[count2] = card;
                        count2++;
                    }
                    foreach (GameObject card in movetoHand2) {
                        card.transform.SetParent(GameObject.Find("Hand").transform);
                        card.transform.position = new Vector3(1, 0, 0);
                        card.transform.tag = "HandCard";
                        card.GetComponent<LayoutElement>().ignoreLayout = false;
                    }
                    break;  
                default:    
                    break;
            }
            cardsonField[0].transform.position = new Vector3(1, 0, 0);
            cardsonField[0].transform.SetParent(GameObject.Find("Trash").transform);
            cardsonField[0].transform.tag = "TrashCard";
            cardsonField[0].GetComponent<LayoutElement>().ignoreLayout = false;
        } 
    }

    public bool getPlayerTurn() {
        return playerTurn;
    }

    public void ChangeTurn() {
        turn++;
        if (playerTurn) {
            playerTurn = false;
            if (enemyImmune)
                enemyImmune = false;
            GameObject[] cardsonHand = GameObject.FindGameObjectsWithTag("HandCard");
            if (cardsonHand.Length > 6) {
                for (int i = 6; i < cardsonHand.Length; i++) {
                    cardsonHand[i].transform.position = new Vector3(1, 0, 0);
                    cardsonHand[i].transform.SetParent(GameObject.Find("Trash").transform);
                    cardsonHand[i].transform.tag = "TrashCard";
                    cardsonHand[i].GetComponent<LayoutElement>().ignoreLayout = false;
                }
            }
            OpponentTurn();
        } else {
            if (playerImmune)
                playerImmune = false;
            playerTurn = true;
            TMP_Text energyText = energy.GetComponent<TMP_Text>();

            switch (turn) {
                case 1:
                    energynum = 1;
                    energyText.text = "1";
                    break;
                case 2:
                    energynum = 2;
                    energyText.text = "2";
                    break;
                case 3:
                    energynum = 3;
                    energyText.text = "3";
                    break;
                case 4:
                    energynum = 4;
                    energyText.text = "4";
                    break;
                case 5:
                    energynum = 5;
                    energyText.text = "5";
                    break;
                case 6:
                    energynum = 6;
                    energyText.text = "6";
                    break;
                case 7:
                    energynum = 7;
                    energyText.text = "7";
                    break;
                case 8:
                    energynum = 8;
                    energyText.text = "8";
                    break;
                case 9:
                    energynum = 9;
                    energyText.text = "9";
                    break;
                default:    
                    energynum = 9;
                    energyText.text = "9";
                    break;
            }
            int count = 0;
            foreach (GameObject card in deck) {
                if (count == 1) {
                    break;
                }
                card.transform.SetParent(GameObject.Find("Hand").transform);
                card.transform.position = new Vector3(1, 0, 0);
                card.transform.tag = "HandCard";
                card.GetComponent<LayoutElement>().ignoreLayout = false;
                count++;
            }   

        }
    }

    private void OpponentTurn () {
        StartCoroutine(wait(0.5f));
        switch (turn) {
            case 1:
                if (!playerImmune) {
                    playerHealth -= 10;
                }
                break;
            case 2:
                if (!playerImmune) {
                    playerHealth -= 10;
                }
                break;
            case 3:
                if (!playerImmune) {
                    playerHealth -= 20;
                }
                break;
            case 4:
                if (!playerImmune) {
                    playerHealth -= 20;
                }
                break;
            case 5:
                if (!playerImmune) {
                    playerHealth -= 40;
                }
                break;
            case 6:
                if (!playerImmune) {
                    playerHealth -= 40;
                }
                break;
            case 7:
                if (!playerImmune) {
                    playerHealth -= 60;
                }
                break;
            case 8:
                if (!playerImmune) {
                    playerHealth -= 60;
                }
                break;
            case 9:
                if (!playerImmune) {
                    playerHealth -= 60;
                }
                break;
            default:    
                if (!playerImmune) {
                    playerHealth -= 60;
                }
                break;
        }
        StartCoroutine(wait(0.5f));
        ChangeTurn();
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerTurn) {
            playButton.GetComponent<Button>().interactable = false;
            endTurnButton.GetComponent<Button>().interactable = false;
        } else if (playerTurn) {
            playButton.GetComponent<Button>().interactable = true;
            endTurnButton.GetComponent<Button>().interactable = true;
        }
        
        deck = GameObject.FindGameObjectsWithTag("DeckCard");
        //if number of cards in deck is 0, reshuffle all trash cards into deck
        if (deck.Length == 0) {
            GameObject[] trash = GameObject.FindGameObjectsWithTag("TrashCard");
            foreach (GameObject card in trash) {
                card.transform.SetParent(GameObject.Find("Deck").transform);
                card.transform.position = new Vector3(1, 0, 0);
                card.transform.tag = "DeckCard";
                card.GetComponent<LayoutElement>().ignoreLayout = false;
            }
            ShuffleDeck();
        }

        playerhealthBar = playerhealthBar.GetComponent<Slider>();
        playerhealthBar.value = playerHealth;
        enemyhealthBar = enemyhealthBar.GetComponent<Slider>();
        enemyhealthBar.value = enemyHealth;

        if (playerHealth <= 0) {
            playerHealth = 0;
            playerhealthText.text = playerHealth.ToString();
            //player loses
        } else if (enemyHealth <= 0) {
            //player wins
            enemyHealth = 0;
            enemyhealthText.text = enemyHealth.ToString();
        }

        playerhealthText.text = playerHealth.ToString();
        enemyhealthText.text = enemyHealth.ToString();
    }

    IEnumerator wait(float sec) {
        yield return new WaitForSeconds(sec);
    }
}


