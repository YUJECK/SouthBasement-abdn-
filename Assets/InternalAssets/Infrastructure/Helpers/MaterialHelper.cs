using SouthBasement.Helpers;
using UnityEngine;

namespace SouthBasement.Helpers
{
    public sealed class MaterialHelper
    {
        public MaterialHelper(Material defaultMaterial)
        {
            OutlineMaterial = Resources.Load<Material>(ResourcesPathHelper.OutlineMaterial);
            DefaultMaterial = defaultMaterial;
        }
        
        public Material DefaultMaterial { get; private set; }
        public Material OutlineMaterial { get; private set; }
    }
}