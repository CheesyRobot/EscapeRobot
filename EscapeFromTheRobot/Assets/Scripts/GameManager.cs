using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerMovement playerMovement;
    private void Awake()
    {
        Instance = this;
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement reference is not set in GameManager.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        if (playerMovement == null) return;
        playerMovement.Respawn();
    }
}
