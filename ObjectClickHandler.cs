using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectClickHandler : MonoBehaviour
{
    [Header("UI Referansları")]
    public TextMeshProUGUI scoreText;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public GameObject congratsPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Ayarlar")]
    public int pointPerTrash = 10;

    private int score = 0;
    private int trashCount = 0;
    private int totalTrash = 0;
    private Camera arCamera;

    void Start()
    {
        score = 0;
        trashCount = 0;
        totalTrash = GameObject.FindGameObjectsWithTag("Trash").Length;
        arCamera = Camera.main;

        if (scoreText != null) scoreText.text = "Puan: 0";
        if (infoPanel != null) infoPanel.SetActive(false);
        if (congratsPanel != null) congratsPanel.SetActive(false);

        Debug.Log("Toplam çöp: " + totalTrash);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            if (infoPanel != null && infoPanel.activeSelf)
                return;

            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            Debug.Log("Dokunuldu: " + touch.position);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name + " Tag: " + hit.collider.gameObject.tag);
                HandleObjectTouch(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Hiçbir şeye çarpmadı");
            }
        }
    }

    void HandleObjectTouch(GameObject obj)
    {
        if (obj.CompareTag("Animal"))
        {
            ShowAnimalInfo(obj.name);
        }
        else if (obj.CompareTag("Trash"))
        {
            CollectTrash(obj);
        }
    }

    void ShowAnimalInfo(string animalName)
    {
        if (infoPanel == null) return;
        infoText.text = "DENİZ KAPLUMBAĞASI\n\nDeniz kaplumbağaları okyanuslarda yaşar. Plastik atıklar onların hayatını tehdit eder.";
        infoPanel.SetActive(true);
    }

    void CollectTrash(GameObject obj)
    {
        score += pointPerTrash;
        trashCount++;
        if (scoreText != null) scoreText.text = "Puan: " + score;
        Destroy(obj);

        if (trashCount >= totalTrash)
        {
            if (congratsPanel != null) congratsPanel.SetActive(true);
            if (finalScoreText != null) finalScoreText.text = "Toplam Puanın\n" + score;
        }
    }

    public void CloseInfo()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}