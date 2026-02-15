using UnityEngine;
using UnityEngine.InputSystem;

public class UIButtons : MonoBehaviour
{
    public InputActionReference menuButton;
    public GameObject restartMenu;

    bool menuActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButton.action.IsPressed())
        {
            if (menuActive) restartMenu.SetActive(false);
            else restartMenu.SetActive(true);
        }
    }
}
