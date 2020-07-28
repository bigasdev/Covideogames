using UnityEngine;


[CreateAssetMenu(fileName = "New Goal", menuName = "Goals")]
public class Goals : ScriptableObject
{
    [System.Serializable]

    public struct HighScore
    {
        public bool activated;
        public float highScore;
    }
    [System.Serializable]

    public struct PeopleMasked
    {
        public bool activated;
        public int peopleMasked;
    }
    [System.Serializable]
    public struct Survival
    {
        public bool activated;
        public float timeSurvived;
    }

    [System.Serializable]
    public struct SendHospital
    {
        public bool activated;
        public int peopleSent;
    }
    public HighScore highScore;
    public PeopleMasked peopleMasked;
    public Survival survival;
    public SendHospital sendHospital;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
