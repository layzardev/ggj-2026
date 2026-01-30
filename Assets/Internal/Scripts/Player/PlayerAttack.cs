using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public void OnShootWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            PlayerProperties.Instance.PlayerWeapon.ShootWeapon();
        }
            
    }


}
