using UnityEngine;

namespace AA
{
    public class CharacterWeapon : MonoBehaviour
    {
        private WeaponPivot _weaponPivot;

        private Weapon _equppiedWeapon;

        private void Awake()
        {
            _weaponPivot = GetComponentInChildren<WeaponPivot>();
        }

        public void Equip(Weapon weapon)
        {
            UnEquip();

            _equppiedWeapon = weapon;

            weapon.Equip();
        }

        private void UnEquip()
        {
            if (_equppiedWeapon)
            {
                _equppiedWeapon.UnEquip();
            }

            _equppiedWeapon = null;
        }

        public void launch()
        {
            _equppiedWeapon.Launch();
        }

    }
}
