using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject infoPanel;
    public string targetTag = "Turtle";

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        Debug.Log("BAŞLADI - ObjectClickHandler (New Input System)");
        infoPanel?.SetActive(false);
    }

    void Update()
    {
        // Yeni Input System - dokunuş
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                HandleClick(touch.position.ReadValue());
                return;
            }
        }

        // Yeni Input System - mouse (Editor'da test için)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick(Mouse.current.position.ReadValue());
        }
    }

    void HandleClick(Vector2 screenPos)
    {
        Debug.Log($"[CLICK] Tıklandı: {screenPos}");

        Ray ray = _cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log($"[HIT] Obje: {hit.collider.name} | Tag: {hit.collider.tag}");

            if (hit.collider.CompareTag(targetTag))
            {
                Debug.Log("[PANEL] InfoPanel açılıyor!");
                infoPanel?.SetActive(true);
            }
            else
            {
                infoPanel?.SetActive(false);
            }
        }
        else
        {
            Debug.Log("[MISS] Hiçbir şeye çarpmadı.");
        }
    }
}