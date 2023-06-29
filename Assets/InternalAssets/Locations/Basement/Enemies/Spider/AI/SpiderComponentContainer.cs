using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderComponentContainer
    {
        public SpiderMovement SpiderMovement { get; private set; }
        public SpiderWeaver SpiderWeaver { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public SpiderAnimator Animator { get; private set; }
        public SpiderAudioPlayer AudioPlayer { get; private set; }

        public SpiderComponentContainer(GameObject masterObject)
        {
            SpiderMovement = masterObject.GetComponent<SpiderMovement>();
            TargetSelector = masterObject.GetComponentInChildren<TargetSelector>();
            SpiderWeaver = masterObject.GetComponentInChildren<SpiderWeaver>();
            Animator = new SpiderAnimator(masterObject.GetComponentInChildren<Animator>());
            AudioPlayer = masterObject.GetComponentInChildren<SpiderAudioPlayer>();
        }
    }
}