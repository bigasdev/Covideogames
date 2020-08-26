using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Text titleText;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        if (GameStats.gameStats.score < 10)
        {
            titleText.text = "Você não está se cuidando e está pondo em risco a vida dos outros";
        }
        else if (GameStats.gameStats.score < 30)
        {
            titleText.text = "Você precisa ter mais cuidados, todos têm que usar máscara!";
        }
        else if (GameStats.gameStats.score < 50)
        {
            titleText.text = "Você está se esforçando, mas o vírus não dá trégua!";
        }
        else if (GameStats.gameStats.score < 80)
        {
            titleText.text = "Você está sendo cuidadoso, continue assim! ";
        }
        else if (GameStats.gameStats.score >= 150)
        {
            titleText.text = "Você é um craque da saúde!";
        }


    }

}
