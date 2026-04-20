using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Метод для перехода в магазин
    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    // Метод для возврата в главное меню
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
