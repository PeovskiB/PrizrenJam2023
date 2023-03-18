using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Transform ui_parent;
    public Item data;
    public RectTransform rect_trans;
    public ItemSlotUI slot;
    private CanvasGroup canvas_gr;
    private Image image;
    private TextMeshProUGUI text;
    private void Awake()
    {
        rect_trans = GetComponent<RectTransform>();
        canvas_gr = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnDisable()
    {
        if (data != null)
            data.OnQuantityChange -= SetText;
    }
    public void Setup(Item new_data, Transform new_ui_parent)
    {
        ui_parent = new_ui_parent;
        data = new_data;
        data.OnQuantityChange += SetText;
        data.QuantityAdd(0);
        image.sprite = new_data.data.icon;
    }
    private void SetText(int new_qty)
    {
        string txt = "";
        if (new_qty != 1)
            txt = new_qty.ToString();
        text.text = txt;
    }
    public void ResetPosition()
    {
        slot.PivotItem(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas_gr.blocksRaycasts = false;
        transform.SetParent(ui_parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect_trans.position = eventData.position;
        canvas_gr.alpha = 0.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvas_gr.blocksRaycasts = true;
        canvas_gr.alpha = 1f;

        if (slot != null && (eventData.pointerCurrentRaycast.gameObject == null || !eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlotUI>()))
            ResetPosition();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (slot) slot.OnDrop(eventData);
    }
}