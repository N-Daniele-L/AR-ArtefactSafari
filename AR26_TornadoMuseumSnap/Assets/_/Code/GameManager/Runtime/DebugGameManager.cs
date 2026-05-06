using TMPro;
using UnityEngine;

namespace GameManager.Runtime
{
    public class DebugGameManager : MonoBehaviour
    {

        #region Main Methods

        public void DisplayChrono(float time)
        {
            _textTimer.text = time.ToString("F1");
        }

        public void DisplayGameCycle(string cycle)
        {
            _textGameCycle.text = cycle;
        }

        #endregion

        #region Private

        [SerializeField] private TMP_Text _textTimer;
        [SerializeField] private TMP_Text _textGameCycle;

        #endregion
        
    }
}
