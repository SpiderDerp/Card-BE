using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardPhysics : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    private Vector3 originalPosition;
    private LayoutElement layoutElement;
    public Rigidbody2D obj;
    public GameObject card;
    private string parent;
    private GameObject[] cardsinHand;
    private GameObject[] cardsonField;
    private bool changed = false;
    public GameObject gameCam;

    void Start()
    {
        //set the object to be kinematic
        obj = GetComponent<Rigidbody2D>();
        layoutElement = GetComponent<LayoutElement>();
        obj.isKinematic = true;
        //store original position
        originalPosition = transform.position;  
        parent = card.transform.parent.name;

        //get all cards in hand
        cardsinHand = GameObject.FindGameObjectsWithTag("HandCard");
    }

    // Update is called once per frame
    void Update()
    {
        //get parent
        
        parent = card.transform.parent.name;

    }
    void OnMouseEnter() {
        //move object up a bit
        //ignoreLayout for all objects in cards in hand
        cardsinHand = GameObject.FindGameObjectsWithTag("HandCard");
        cardsonField = GameObject.FindGameObjectsWithTag("FieldCard");
        foreach (GameObject card in cardsinHand) {
            card.GetComponent<LayoutElement>().ignoreLayout = true;
        }
        foreach (GameObject card in cardsonField) {
            card.GetComponent<LayoutElement>().ignoreLayout = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f , transform.position.z);
    }

    void OnMouseExit() {
        //move object back to original position
        cardsinHand = GameObject.FindGameObjectsWithTag("HandCard");
        cardsonField = GameObject.FindGameObjectsWithTag("FieldCard");
        if (!changed) {
            transform.position = originalPosition;
        } else {
            changed = false;
        }
        foreach (GameObject card in cardsinHand) {
            card.GetComponent<LayoutElement>().ignoreLayout = false;
        }
        foreach (GameObject card in cardsonField) {
            card.GetComponent<LayoutElement>().ignoreLayout = false;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameCam.GetComponent<Game>().getPlayerTurn()) {
            cardsonField = GameObject.FindGameObjectsWithTag("FieldCard");
            //move card to Field
            if (parent == "Field") {
                //move card to hand
                transform.position = originalPosition;
                card.transform.SetParent(GameObject.Find("Hand").transform);
                changed = true;
                card.transform.position = new Vector3(1, 0, 0);
                layoutElement.ignoreLayout = false;
                transform.tag = "HandCard";
            } else {
                //move card to field
                if (cardsonField.Length == 0) {
                    transform.position = originalPosition;
                    card.transform.SetParent(GameObject.Find("Field").transform);
                    changed = true;
                    card.transform.position = new Vector3(1, 0, 0);
                    transform.tag = "FieldCard";
                    layoutElement.ignoreLayout = false;
                }
            }
        }
    }
}
