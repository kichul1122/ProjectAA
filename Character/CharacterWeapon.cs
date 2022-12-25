using System;
using UniRx;
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

            Observable.Interval(TimeSpan.FromSeconds(0.2d)).Subscribe(_ =>
            {
                //Character newCharacter = pool.Rent();
                //newCharacter.Observable.OnRemoveObservable().Subscribe(character => pool.Return(character)).AddTo(this);

                //newCharacter.Construct().SetParent(transform).Teleport(setting.spawnPosition);

                //Managers.Object.AddEnemy(newCharacter);

                Launch();


            }).AddTo(this);
        }

        public CharacterWeapon Equip(Weapon weapon)
        {
            UnEquip();

            _equppiedWeapon = weapon;

            weapon.transform.SetParent(_weaponPivot.transform, false);

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

            _equppiedWeapon.Launch();
        }

    }
}
