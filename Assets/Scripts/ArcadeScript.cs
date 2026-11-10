using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class ArcadeScript : MonoBehaviour
{
    [SerializeField] private InputAction EscapeKey; 
    [SerializeField] private InputAction RestartKey; 
    public SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        EscapeKey.Enable();
        RestartKey.Enable();

        spawnManager = gameObject.GetComponent<SpawnManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(EscapeKey.WasPerformedThisFrame()){
            Application.Quit();
        }
           //Restarts when the restart key is pressed
        if (RestartKey.WasPressedThisFrame() && spawnManager.player == null)
        {
            RestartActiveScene();
        }
    }

    public void RestartActiveScene()
    {
        // Resets the Time.timeScale back to normal in case the game was paused
        Time.timeScale = 1f; 
        
        // Reloads the currently loaded active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    }

