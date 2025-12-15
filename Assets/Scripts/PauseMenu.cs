using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool gamePaused = false;

    public InputActionAsset actions;   // arrastre DefaultInputActions aquí
    public string actionMap = "UI";
    public string actionName = "Pause";

    private InputAction pauseAction;

    private void Awake()
    {
        pauseAction = actions.FindActionMap(actionMap, true).FindAction(actionName, true);
    }

    private void OnEnable()
    {
        pauseAction.performed += OnPause;
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPause;
        pauseAction.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (gamePaused) ResumeGame();
        else PauseGame();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
