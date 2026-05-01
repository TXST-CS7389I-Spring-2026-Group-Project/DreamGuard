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
    /// method (e.g. DreamGuardPassthrough.Toggle). Clicking the active button
    /// a second time calls onSelect again — wire accordingly for toggle behaviour.
    ///
    /// To add a new passthrough technique: duplicate an existing button child
    /// of MenuPanel in the prefab, rename it, update buttonLabel, and wire
    /// onSelect to the new technique. No script changes required.
    /// </summary>
    public class DreamGuardMenuButton : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private string buttonLabel = "Option";

        [Header("Action")]
        public UnityEvent onSelect;

        [Header("Colors")]
        [SerializeField] private Color normalColor = new Color(0.12f, 0.12f, 0.20f);
        [SerializeField] private Color hoverColor  = new Color(0.28f, 0.40f, 0.65f);
        [SerializeField] private Color activeColor = new Color(0.18f, 0.52f, 1.00f);

        private Renderer _renderer;
        private MaterialPropertyBlock _mpb;
        private bool _isActive;

        public string ButtonLabel => buttonLabel;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _mpb = new MaterialPropertyBlock();
            ApplyLabel();
        }

        private void ApplyLabel()
        {
            var mesh = GetComponentInChildren<TextMesh>();
            if (mesh != null)
                mesh.text = buttonLabel;
        }

        public void SetHovered(bool hovered)
        {
            ApplyColor(hovered ? hoverColor : (_isActive ? activeColor : normalColor));
        }

        public void SetActiveState(bool active)
        {
            _isActive = active;
            ApplyColor(active ? activeColor : normalColor);
        }

        public void Select() => onSelect?.Invoke();

        private void ApplyColor(Color color)
        {
            if (_renderer == null) return;
            _renderer.GetPropertyBlock(_mpb);
            _mpb.SetColor("_BaseColor", color);  // URP lit/unlit property
            _renderer.SetPropertyBlock(_mpb);
        }
    }
}
