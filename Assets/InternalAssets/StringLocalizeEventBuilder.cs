using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace SouthBasement
{
    public class StringLocalizeEventBuilder : MonoBehaviour
    {
        public GameObject MasterObject;
        
        [NaughtyAttributes.Button()]
        private void Build()
        {
            var children = MasterObject.GetComponentsInChildren<LocalizeStringEvent>();

            foreach (var child in children)
            {
                child.OnUpdateString.RemoveAllListeners();
                child.OnUpdateString.AddListener((text) => child.GetComponent<TMP_Text>().text = text);
            }
        }
    }
}
