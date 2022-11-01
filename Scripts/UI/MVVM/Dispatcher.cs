using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AA.Assets._Scripts.UI.MVVM
{
    public class Dispatcher : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private CharacterData characterData;

        [SerializeField]
        private Service service;

        private ViewModel viewModel;

        [SerializeField]
        private View view;
        
        private void Start()
        {
            //characterDatas = Managers.Data.Character.Datas

            characterData = new CharacterData();

            service.Constructor(characterData);
            
            ViewModel viewModel = new ViewModel(service, characterData);

            view.Binding(viewModel);
        }

        private void OnDestroy()
        {
            viewModel.Dispose();
        }
    }
}
