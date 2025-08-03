using UnityEngine;

public class Monitor : Interactable_y
{
    [SerializeField] GameObject screenShards;
    [SerializeField] GameObject screenON;
    [SerializeField] GameObject monitorSmoke;
    [SerializeField] PlayerInventory_y inventory;

    protected override void Interact()
    {
        if (inventory.SearchItem("Hammer") != -1)
        {
            BreakScreen();
        }
    }

    private void BreakScreen()
    {
        screenON.SetActive(false);
        monitorSmoke.SetActive(true);
        screenShards.SetActive(true);
        Invoke(nameof(LoadNextScene), 5f);
        }

        private void LoadNextScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
            );
        }
}
