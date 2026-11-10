using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public float wallSpeed = 20;
    public MoveForward moveFwd;

    public GameObject ballprefab;
    public GameObject Wallprefab;
    public int ballCount;

    private GameObject wall;


    public GameObject player;

    public TurnOnGravity turnOnGravity;
    
    public List<GameObject> ballList;


    public GameObject UIMenu;

    public bool gameStarted = false;

    public bool playerEnabled = false;


    public int score;

    public TextMeshProUGUI scoreTextBox;
    public TextMeshProUGUI gameOverText;

    public GameObject otherPlayer;


    [SerializeField] private InputAction startButton;
    [SerializeField] private InputAction playerButton; 


    
    void Start()
    {
        startButton.Enable();
        playerButton.Enable();
        ballList = new List<GameObject>();
        turnOnGravity = player.GetComponent<TurnOnGravity>();
        gameStarted = false;
        score = 0;
        gameOverText.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if(player != null){
        player.SetActive(playerEnabled);
        scoreTextBox.gameObject.SetActive(playerEnabled);
        GameLoop();
        scoreTextBox.text = "Score: "+score;
        if(startButton.WasPressedThisFrame()){
            StartGame();
        }
        if(playerButton.WasPerformedThisFrame()){
            PlayerPicker();
        }
        }
        CheckGameOver();

    }
    //ball spawn points
    Vector3 LeftArmBall(){
        float ballX,ballY,ballZ;
        float Randomdist = Random.Range(1f,1.5f);
        ballZ = player.transform.position.z;
        ballY = Random.Range(1.3f,4f);
        ballX = player.transform.position.x + (Randomdist * -1);
        return new Vector3(ballX,ballY,ballZ);
    }
    Vector3 RightArmBall(){
        float ballX,ballY,ballZ;
        float Randomdist = Random.Range(1f,1.5f);
        ballZ = player.transform.position.z;
        ballY = Random.Range(1.3f,4f);
        ballX = player.transform.position.x + (Randomdist);
        return new Vector3(ballX,ballY,ballZ);
    }
    Vector3 LeftLegBall(){
        float ballX,ballY,ballZ;
        float Randomdist = Random.Range(1f,1.5f);
        ballZ = player.transform.position.z;
        ballY = Random.Range(0f,1.4f);
        ballX = player.transform.position.x + (Randomdist * -1);
        return new Vector3(ballX,ballY,ballZ);
    }
    Vector3 RightLegBall(){
        float ballX,ballY,ballZ;
        float Randomdist = Random.Range(1f,1.5f);
        ballZ = player.transform.position.z;
        ballY = Random.Range(0f,1.4f);
        ballX = player.transform.position.x + (Randomdist);
        return new Vector3(ballX,ballY,ballZ);
    }
    //wall spawn point
    Vector3 WallSpawnPoint(){
        float wallX,wallY,wallZ;
        wallZ = 10f;
        wallY = 3.3f;
        wallX = player.transform.position.x;
        return new Vector3(wallX,wallY,wallZ);
    }
    //spawn 4 balls at all 4 limbs
    void SpawnBalls(){
        ballList.Add(Instantiate(ballprefab,LeftArmBall(),ballprefab.transform.rotation) as GameObject);
        ballList.Add(Instantiate(ballprefab,RightArmBall(),ballprefab.transform.rotation) as GameObject);
        ballList.Add(Instantiate(ballprefab,LeftLegBall(),ballprefab.transform.rotation) as GameObject);
        ballList.Add(Instantiate(ballprefab,RightLegBall(),ballprefab.transform.rotation) as GameObject); 
    }
    
    public void StartGame(){
        //check if at least 1 player is enabled
        if(playerEnabled && !gameStarted){
        gameStarted = true;
        UIMenu.SetActive(false);
        wallSpeed = 1;
        wall = Instantiate(Wallprefab,WallSpawnPoint(),Wallprefab.transform.rotation);
        moveFwd = wall.GetComponent<MoveForward>();
        moveFwd.SetSpeed(wallSpeed);
        SpawnBalls();
        }
    }

    public void GameLoop(){
        if(gameStarted && playerEnabled){
        ballCount = FindObjectsByType<SphereCollider>(FindObjectsSortMode.None).Length;
        if(CheckTouching()){
            AddScore();
            DestroyBallsandWall();
            
            if(!turnOnGravity.isGravityOn()){
                SpawnBalls();
                wall = Instantiate(Wallprefab,WallSpawnPoint(),Wallprefab.transform.rotation);
                moveFwd = wall.GetComponent<MoveForward>();
                wallSpeed += 0.4f;
                moveFwd.SetSpeed(wallSpeed);
            }
        }
        }

    }
    public bool CheckTouching(){
        bool allTouched = ballList.All(item => 
        item != null && 
        item.GetComponent<BallHandler>() != null && 
        item.GetComponent<BallHandler>().IsTouched);

        if (allTouched){return true;}
        else{return false;}
    }

    public void PlayerPicker(){
        if(!gameStarted){playerEnabled = !playerEnabled;}
    }

    public void DestroyBallsandWall(){
        Destroy(wall);
        foreach (GameObject obj in ballList){
            if (obj != null){Destroy(obj);}}
            ballList.Clear();
    }
    //increase score by 1
    public void AddScore(){
        score++;
    }


    public void CheckGameOver(){
        if(player != null){
        if(player.transform.position.y <= -10){
            Destroy(player);
            gameStarted = false;
            DestroyBallsandWall();
            scoreTextBox.gameObject.SetActive(false);
            gameOverText.text += "\n"+player.name + " score: "+score;
        }
        }
        if((player == null && otherPlayer == null) || player == null && !otherPlayer.activeInHierarchy){
            gameOverText.gameObject.SetActive(true);

        }
    }
}
