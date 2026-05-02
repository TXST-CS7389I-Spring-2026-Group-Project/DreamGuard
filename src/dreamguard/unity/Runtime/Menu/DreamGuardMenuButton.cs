using UnityEngine;
using UnityEngine.Events;

namespace DreamGuard
{
    /// <summary>
    /// A single selectable button in the DreamGuard floating menu.
    ///
    /// Place a GameObject with this component as a child of the menu's
    /// MenuPanel; <see cref="DreamGuardMenu"/> discovers it automatically.
    ///
    /// Wire <see cref="onSelect"/> in the Inspector to call any passthrough
    /// method (e.g. DreamGuardWindowedPassthrough.Toggle). Clicking the active
    /// button a second time calls onSelect again — wire accordingly for toggle.
    /// </summary>
    public class DreamGuardMenuButton : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private string buttonLabel = "Option";

        [Header("Action")]
        public UnityEvent onSelect;

        [Header("Colors")]
        [SerializeField] private Color normalColor = new Color(0.35f, 0.35f, 0.35f);
        [SerializeField] private Color hoverColor  = new Color(0.20f, 0.45f, 0.90f);
        [SerializeField] private Color activeColor = new Color(0.00f, 0.25f, 0.60f);

        private Renderer _renderer;
        private Material _matInstance;
        private bool _isActive;

        public string ButtonLabel => buttonLabel;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            if (_renderer != null)
                _matInstance = _renderer.material; // per-instance copy, avoids shared-material mutation
            ApplyLabel();
            ApplyColor(normalColor);
        }

        private void ApplyLabel()
        {
            var mesh = GetComponentInChildren<TextMesh>();
            if (mesh != null) mesh.text = buttonLabel;
        }

        public void SetHovered(bool hovered) =>
            ApplyColor(hovered ? hoverColor : (_isActive ? activeColor : normalColor));

        public void SetActiveState(bool active)
        {
            _isActive = active;
            ApplyColor(active ? activeColor : normalColor);
        }

        public void Select() => onSelect?.Invoke();

        private void ApplyColor(Color color)
        {
            if (_matInstance == null) return;
            // _BaseColor is the URP Unlit main color; set it directly.
            _matInstance.SetColor("_BaseColor", color);
        }
    }
}
