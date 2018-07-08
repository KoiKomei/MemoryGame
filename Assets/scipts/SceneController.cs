using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh moveLabel;
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    private MemoryCard _first;
    private MemoryCard _second;
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    public bool canUncover {
        get { return _second == null; }
    }

    void Start () {

        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.setCard(id, images[id]);
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
        
        

	}


    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    public void unCovered(MemoryCard card) {
        if (_first == null)
        {
            _first = card;

        }
        else {
            _second = card;
            StartCoroutine(CardsCheck()); 
        }
    }
    private int _score = 0;
    private int _moves = 0;
    private IEnumerator CardsCheck() {
        if (_first.id == _second.id)
        {
            _score++;
            _moves++;
        }
        else {
            yield return new WaitForSeconds(1.0f);
            _first.Cover();
            _second.Cover();
            _moves++;

        }
        _first = null;
        _second = null;
    }

	// Update is called once per frame
	void Update () {
        scoreLabel.text = "Score: " + _score;
        moveLabel.text = "Moves: " + _moves;
	}

    public void Restart() {
        SceneManager.LoadScene("memory");
    }

}
