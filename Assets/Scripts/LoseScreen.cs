using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public bool lost = false;
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


        if (lost)
        {
            Time.timeScale = 0;
            canvasGroup.alpha += alphaRate;
            UI.enabled = false;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            if (score.GetScore() < 10)
            {
                titleText.text = "Você não está se cuidando e está pondo em risco a vida dos outros";
            }
            else if (score.GetScore() < 30)
            {
                titleText.text = "Você precisa ter mais cuidados, todos têm que usar máscara!";
            }
            else if (score.GetScore() < 50)
            {
                titleText.text = "Você está se esforçando, mas o vírus não dá trégua!";
            }
            else if (score.GetScore() < 80)
            {
                titleText.text = "Você está sendo cuidadoso, continue assim! ";
            }
            else if (score.GetScore() >= 150)
            {
                titleText.text = "Você é um craque da saúde!";
            }

        }

    }


}
