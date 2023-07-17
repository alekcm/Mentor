using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject[] needToMove;
    public float moveSpeed = 0.1f;
    public TMPro.TextMeshProUGUI coinsCountText;
    public int coinsCount = 0;
    public GameObject enemy;
    public GameObject coin;
    public Transform topEnemySpawnPos;
    public Transform downEnemySpawnPos;
    public GameObject startedHint;
    public bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !gameStarted)
        {
            StartGame();
        }
    }

    private void FixedUpdate()
    {
        if (gameStarted)
        for (int i = 0; i < needToMove.Length; i++)
            needToMove[i].transform.position = new Vector3 (needToMove[i].transform.position.x + (moveSpeed * Time.fixedDeltaTime), needToMove[i].transform.position.y, needToMove[i].transform.position.z);
        
    }
    public void TakeCoin()
    {
        coinsCount++;
        coinsCountText.text = coinsCount.ToString();
    }
    public void StartGame()
    {
        gameStarted = true;
        startedHint.SetActive(false);
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnCoin());
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator SpawnEnemy()
    {
        while (true)
        {
            for (int i = -1; i <= coinsCount; i += 2)
            {
                GameObject newEnemy = Instantiate(enemy, new Vector3(topEnemySpawnPos.position.x, Random.Range(topEnemySpawnPos.position.y, downEnemySpawnPos.position.y), topEnemySpawnPos.position.z), Quaternion.identity, null);
                Animator animator = newEnemy.transform.GetChild(0).GetComponent<Animator>();
                animator.speed = Random.Range(1f,2f);
                //animator.PlayInFixedTime("Enemy", -1, Random.Range(0f, 0.5f));
                    yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));
            }
            
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
    public IEnumerator SpawnCoin()
    {
        while (true)
        {
            Instantiate(coin, new Vector3(topEnemySpawnPos.position.x, Random.Range(topEnemySpawnPos.position.y, downEnemySpawnPos.position.y), topEnemySpawnPos.position.z), Quaternion.identity, null);
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }
}
