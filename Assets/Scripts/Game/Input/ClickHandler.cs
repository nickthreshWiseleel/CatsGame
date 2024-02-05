using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class ClickHandler : MonoBehaviour, CustomInput.IPlayerActions
    {
        private CustomInput _input;

        private void Awake()
        {
            _input = new();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Click.performed += OnClick;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Click.performed -= OnClick;
        }

        public void OnClick(InputAction.CallbackContext value)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform && hit.transform.TryGetComponent(out IHitable hitable))
            {
                hitable.Hit();
            }
        }
    }
}