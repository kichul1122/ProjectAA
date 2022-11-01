using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AA
{
    public class MVPView : MonoBehaviour
    {
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI GradeText;

        public DefaultGridView defaultGridView;

        public void SetLevelText(string text)
        {
            LevelText.text = text;
        }

        public void SetGradeText(string text)
        {
            GradeText.text = text;
        }

        public void SetDatas(IList<DefaultItemData> datas)
        {
            defaultGridView.UpdateContents(datas);
        }
    }    
}
