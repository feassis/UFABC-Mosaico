using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageMosaicoGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI winTimerText;
    [SerializeField] private GameObject winPopup;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource VictorySound;
    float timer;

    public AudioClip InicioAudio;

    bool partesEmbaralhadas = false;

    public DragAndDrop Parte;
    public Image LocalMarcado;

    public float lmLargura, lmAltura;

    float gameTimer;
    bool gameStarted = false;

    private List<DragAndDrop> dragAndDrops = new List<DragAndDrop>(); 

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 4 && !partesEmbaralhadas)
        {
            EmbaralharPartes();
            FalaPlay();
            partesEmbaralhadas = true;
            gameStarted = true;
        }

        if (gameStarted)
        {
            gameTimer += Time.deltaTime;

            timerText.text = FormatTime(gameTimer);
        }
    }

    private string FormatTime(float seconds)
    {
        if (seconds < 0)
            seconds = 0;

        int minutes = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);

        return $"{minutes:00}:{secs:00}";
    }


    public void CriarLocaisMarcados()
    {

        float numLinhas = 5;
        float numColunas = 5;

        float linha, coluna;

        for(int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();

            posicaoCentro = GameObject.Find("LadoDireito").transform.position;
            linha = i % 5;
            coluna = i / 5;

            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura * (linha-numLinhas / 2),
            posicaoCentro.y - lmAltura*(coluna-numColunas/2),
            posicaoCentro.z);

            Image lm = Instantiate<Image>(LocalMarcado, lmPosicao, Quaternion.identity);
            lm.tag = ""+(i+1);
            lm.name = "LM"+(i+1);

            lm.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    public void CriarPartes()
    {
        float numLinhas = 5;
        float numColunas = 5;

        float linha, coluna;

        for(int i = 0; i < 25; i++)
        {
            Vector3 posicaoCentro = new Vector3();

            posicaoCentro = GameObject.Find("LadoEsquerdo").transform.position;
            linha = i % 5;
            coluna = i / 5;

            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura * (linha-numLinhas / 2),
            posicaoCentro.y - lmAltura*(coluna-numColunas/2),
            posicaoCentro.z);

            DragAndDrop lm = Instantiate<DragAndDrop>(Parte, lmPosicao, Quaternion.identity);
            lm.tag = ""+(i+1);
            lm.name = "Parte"+(i+1);

            lm.transform.SetParent(GameObject.Find("Canvas").transform);

            Sprite[] todasSprites = Resources.LoadAll<Sprite>("DigimonAdventure");
            lm.gameObject.GetComponent<Image>().sprite = todasSprites[i];

            dragAndDrops.Add(lm);
        }
    }

    private void EmbaralharPartes()
    {
        int[] novoArray = new int [25];

        for(int i = 0; i< 25; i++)
        {
            novoArray[i]=i;
        }

        int tmp;

        for(int t = 0; t < 25; t++)
        {
            tmp = novoArray[t];
            int r = Random.Range(t, 10);
            novoArray[t] = novoArray[r];
            novoArray[r] = tmp;
        }

        float linha, coluna, numLinhas, numColunas;

        numLinhas = numColunas = 5;

        for(int i = 0; i < 25; i++)
        {
            linha = (novoArray[i]) %5;
            coluna = (novoArray[i]) / 5;

            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("LadoEsquerdo").transform.position;

            var g = GameObject.Find($"Parte{i + 1}");

            Vector3 novaPos = new Vector3(posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
            posicaoCentro.y - lmAltura *(coluna - numColunas / 2), 
            posicaoCentro.z);

            g.transform.position = novaPos;
            g.GetComponent<DragAndDrop>().PosiçãoInicialPartes();
        }
    }

    void FalaPlay()
    {
        GameObject.Find("totemPlay").GetComponent<TocadorPlay>().PlayPlay();
    }

    private void Awake()
    {
        DragAndDrop.OnPieceCorrectPlacement += DragAndDrop_OnPieceCorrectPlacement;
    }

    private void DragAndDrop_OnPieceCorrectPlacement()
    {
        foreach(var drag in dragAndDrops)
        {
            if (!drag.IsCorrect)
            {
                return;
            }
        }

        WinLoop();
    }

    private void WinLoop()
    {
        gameStarted = false;
        winPopup.SetActive(true);
        winTimerText.text = FormatTime(gameTimer);
        VictorySound.Play();
    }

    void Start()
    {
        CriarLocaisMarcados();
        CriarPartes();
        TocadorInicio.instance.PlayAudio(InicioAudio, 0.5f);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
