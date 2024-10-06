using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 10f;
    public float speedMultiplier = 0.5f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;

    private readonly List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;

    public int foodcounter = 0;
    public TextMeshProUGUI gameStatusText;
    public TMP_Text scoreText;
    public bool ToolRDY = false;

    private float timer = 0;
    private bool shouldReturnToMain = false;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.down;
            }
        }
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2Int.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2Int.left;
            }
        }

        scoreText.text = string.Format("Мусора собрано: {0}", foodcounter - 3);
        if (foodcounter < 12 + 3)
            scoreText.color = Color.yellow;
        else
            scoreText.color = Color.green;

    }

    private void FixedUpdate()
    {
        if (Time.time < nextUpdate) {
            return;
        }

        if (input != Vector2Int.zero) {
            direction = input;
        }
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        foodcounter++;
    }

    public void ResetState()
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y) {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            if (foodcounter > 12 + 2)
            {
                gameStatusText.text = "Вы получили гидротерраформинговую лейку!";
                //HideStatusText();
                for (int i = 0; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                segments.Clear();
                ToolRDY = true;
                GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
                foreach (GameObject food in foodObjects)
                {
                    Destroy(food);
                }
                StartCoroutine(HideStatusText());

                SystemState.ToolReady = ToolRDY; // Сохраняем изменения в памяти
                                    //Переход на сцену с планетой (MainScene)

                //bool ToolRDY = PlayerPrefs.GetInt("ToolRDY", 0) == 1; // Получаем значение ToolRDY в ToolRDY

            }
            else 
            {
                gameStatusText.text = "Game Over!";

                for (int i = 0; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                segments.Clear();
                GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
                foreach (GameObject food in foodObjects)
                {
                    Destroy(food);
                }
                //StartCoroutine(HideStatusText());
                //Переход на сцену с планетой (MainScene)
            }
            
            SystemState.SnakePlayed = true;
            FindObjectOfType<SnakeMainSceneLoader>().shouldReturnToMain = true;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            if (moveThroughWalls) {
                Traverse(other.transform);
            } else {
                ResetState();
            }
        }
    }

    private IEnumerator HideStatusText()
    {
        yield return new WaitForSeconds(3f);
        gameStatusText.text = "";
    }
    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f) {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        } else if (direction.y != 0f) {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }

        transform.position = position;
    }

}
