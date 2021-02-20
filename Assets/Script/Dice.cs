using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {

    public GameObject Dice1;
    public GameObject Dice2;


    private Sprite[] diceSides;
    private SpriteRenderer rend1;
    private SpriteRenderer rend2;
    private int whosTurn = 1;
    private bool coroutineAllowed = true;


	// Use this for initialization
	private void Start () {
        rend1 = Dice1.GetComponent<SpriteRenderer>();
        rend2 = Dice2.GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend1.sprite = diceSides[0];
        rend2.sprite = diceSides[5];
	}

    public void RollDice()
    {
        if (!GameManager.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    /*
    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }
     * */

    private IEnumerator RollTheDice()
    {
        gameObject.GetComponent<Button>().enabled = false;

        coroutineAllowed = false;
        int randomDiceSide1 = 0;
        int randomDiceSide2 = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide1 = Random.Range(0, 6);
            randomDiceSide2 = Random.Range(0, 6);
            rend1.sprite = diceSides[randomDiceSide1];
            rend2.sprite = diceSides[randomDiceSide2];
            yield return new WaitForSeconds(0.05f);
        }

        // Set dice step
        GameManager.diceSideThrown = (randomDiceSide1 + 1) + (randomDiceSide2 + 1);


        if (whosTurn == 1)
        {
            GameManager.MovePlayer(1);
        } else if (whosTurn == -1)
        {
            GameManager.MovePlayer(2);
        }
        whosTurn *= -1;
        coroutineAllowed = true;

        gameObject.GetComponent<Button>().enabled = true;
    }
}
