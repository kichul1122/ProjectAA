using System.Collections;
using TMPro;
using UnityEngine;

namespace AA
{
    public class MVPCellView : MonoBehaviour
    {
        public TextMeshProUGUI IdText;
        public TextMeshProUGUI CountText;

        public void SetIdText(string text)
        {
            IdText.text = text;
        }

        public void SetCountText(string text)
        {
            CountText.text = text;
        }

    }
}