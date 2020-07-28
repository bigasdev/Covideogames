using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public bool won = false;
    CanvasGroup canvasGroup;
    public Canvas UI;
    public Text titleText;
    public CounterScore score;
    float alphaRate = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (won)
        {
            Time.timeScale = 0;
            canvasGroup.alpha += alphaRate;
            UI.enabled = false;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            titleText.text = "Parabéns! Você cumpriu seu objetivo e salvou vidas!";
            
        }

    }


}
