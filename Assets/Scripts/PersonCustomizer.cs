using System;
using UnityEngine;

public class PersonCustomizer : MonoBehaviour
{
    [SerializeField] GameObject[] acessóriosParaCabeca;
    [SerializeField] GameObject[] acessóriosParaCabeçaQueDaPraUsarComCabelosExtra;
    [SerializeField] GameObject[] camisasVestidosEBlusas;
    [SerializeField] GameObject[] casacos;
    [SerializeField] GameObject cabelosBase;
    [SerializeField] GameObject[] cabelosExtra;
    public GameObject boca;
    public GameObject mascara;
    public GameObject pele;


    [SerializeField] Material[] coresDePele;
    [SerializeField] Material[] coresDeRoupa;
    [SerializeField] Material[] coresDeCabelo;

    bool usaChapeu = false;
    [SerializeField] float chanceChapeu;

    bool usaCasaco = false;
    [SerializeField] float chanceCasaco;

    public bool usaMascara = false;

    bool usaCabelo = false;

    bool usaCabeloExtra = false;
    [SerializeField] float chanceCabeloExtra;


    GameObject casaco;
    GameObject cabeloExtra;
    GameObject acessorioCabeca;
    GameObject camisa;



    // Start is called before the first frame update
    void Awake()
    {
        CalculaCabeloExtra();
        CalculaAcessórioCabeca();
        CalcularCabelo();
        CalculaCasaco();
        CalcularCamisa();
        PintaCabelo();
        PintaRoupas();
        PintaPele();

    }

    private void PintaPele()
    {
        CalcularCor(pele, coresDePele);
    }

    private void PintaRoupas()
    {
        if (casaco) CalcularCor(casaco, coresDeRoupa);
        if (camisa) CalcularCor(camisa, coresDeRoupa);
        if (acessorioCabeca) CalcularCor(acessorioCabeca, coresDeRoupa);

    }

    private void PintaCabelo()
    {
        CalcularCor(cabelosBase, coresDeCabelo);
        if (cabeloExtra)
        {
            cabeloExtra.GetComponent<Renderer>().material = cabelosBase.GetComponent<Renderer>().material;
        }
    }

    void CalcularCor(GameObject objeto, Material[] cores)
    {
        objeto.GetComponent<Renderer>().material = cores[UnityEngine.Random.Range(0, cores.Length)];
    }

    private void CalcularCabelo()
    {
        if (!usaChapeu)
        {
            usaCabelo = true;
            cabelosBase.SetActive(true);
        }
    }

    private void CalcularCamisa()
    {
        int indexCamisa = UnityEngine.Random.Range(0, camisasVestidosEBlusas.Length);
        camisa = camisasVestidosEBlusas[indexCamisa];
        camisa.SetActive(true);
    }

    private void CalculaCasaco()
    {
        if (UnityEngine.Random.Range(0, 100) < chanceCasaco)
        {
            
            usaCasaco = true;
            int indexCasaco = UnityEngine.Random.Range(0, casacos.Length);
            casaco = casacos[indexCasaco];
            casaco.SetActive(true);
     
            
        }
    }

    private void CalculaAcessórioCabeca()
    {
        if (UnityEngine.Random.Range(0, 100) < chanceChapeu)
        {
            usaChapeu = true;
            if (usaCabeloExtra)
            {
                int acessório = UnityEngine.Random.Range(0, acessóriosParaCabeçaQueDaPraUsarComCabelosExtra.Length);
                acessorioCabeca = acessóriosParaCabeçaQueDaPraUsarComCabelosExtra[acessório];
                acessorioCabeca.SetActive(true);
            }
            else
            {
                int acessório = UnityEngine.Random.Range(0, acessóriosParaCabeca.Length);
                acessorioCabeca = acessóriosParaCabeca[acessório];
                acessorioCabeca.SetActive(true);
            }
        }
    }

    private void CalculaCabeloExtra()
    {
        if (UnityEngine.Random.Range(0, 100) < chanceCabeloExtra) 
        {
            usaCabeloExtra = true;
            int cabelosExtraEscolhido = UnityEngine.Random.Range(0, cabelosExtra.Length);
            cabeloExtra = cabelosExtra[cabelosExtraEscolhido];
            cabeloExtra.SetActive(true);
        }
    }

    // Update is called once per frame
 
}
