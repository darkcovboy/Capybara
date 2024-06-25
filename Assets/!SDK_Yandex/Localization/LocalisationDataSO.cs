using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class LocalisationDataSO : ScriptableObject
    {
        public List<LocalisationData> LocalisationList;
    }
}