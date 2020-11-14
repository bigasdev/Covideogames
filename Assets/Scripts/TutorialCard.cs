using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//grab text and scene loader
//set phases
//write text for different phases
//next buttton cyles through phases
//if phase is the last one, next button  goes to next scene
public class TutorialCard : MonoBehaviour
{
    public Text text;
    [TextArea(6,10)] [SerializeField] string[] paragraphs;
    int currentParagraph = 0;
    [SerializeField] SceneLoader sceneLoader;

    private void Awake()
    {
        SceneLoader sceneLoader = GetComponent<SceneLoader>();
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = paragraphs[currentParagraph];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        if (currentParagraph < paragraphs.Length - 1)
        {
            currentParagraph++;
            text.text = paragraphs[currentParagraph];
        }
        else
        {
            print(paragraphs.Length);
            sceneLoader.LoadNextScene();
        }
    }
}
