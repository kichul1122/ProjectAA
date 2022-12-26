using System;
using UniRx;
using UnityEngine;

namespace AA
{
    public class CharacterWeapon : MonoBehaviour
    {
        private WeaponPivot _weaponPivot;

        private Weapon _equppiedWeapon;

        public CharacterRotator _rotator;

        private void Awake()
        {
            _rotator = GetComponentInChildren<CharacterRotator>();
            _weaponPivot = GetComponentInChildren<WeaponPivot>();

            Observable.Interval(TimeSpan.FromSeconds(0.2d)).Subscribe(_ =>
            {
                Launch();
            }).AddTo(this);
        }

        public CharacterWeapon Equip(Weapon weapon)
        {
            UnEquip();

            _equppiedWeapon = weapon;

            weapon.SetPivot(_weaponPivot);
            weapon.Equip();

            return this;
        }

        private void UnEquip()
        {
            if (_equppiedWeapon)
            {
                _equppiedWeapon.UnEquip();
            }

            _equppiedWeapon = null;
        }

        public void Launch()
        {
            if (!_equppiedWeapon) return;

            _equppiedWeapon.Launch(_rotator.Forward);
        }

    }
}
