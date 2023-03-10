using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Renderers
{
    public class CompassRenderer : MonoBehaviour
    {
        [SerializeField] private RectTransform compassPointerTransform;

        private readonly List<float> _magicPoints = new();

        private const int AverageMagicPointAccuracy = 10;

        private void Start()
        {
            PhoneDirection.Instance.onCompassChange.AddListener(RenderCompassPoint);
        }

        private void RenderCompassPoint(float newRotation)
        {
            RenderPointer(compassPointerTransform, newRotation, AverageMagicPointAccuracy, _magicPoints);
        }

        private void RenderPointer(RectTransform pointerTransform, float newRotation, int accuracy, List<float> pointsContainer)
        {
            float previousRotationAverage = pointsContainer.Count > 1 ? pointsContainer.Sum() / pointsContainer.Count : 0;
            if (previousRotationAverage - 180 > newRotation) { newRotation += 360; }
            if (previousRotationAverage + 180 < newRotation) { newRotation -= 360; }
            pointsContainer.Add(newRotation);
            if (pointsContainer.Count > accuracy) { pointsContainer.RemoveAt(0); }
            float averageRotation = pointsContainer.Sum() / pointsContainer.Count;
            pointerTransform.rotation = Quaternion.Euler(new Vector3(0, 0, averageRotation));
        }
    }
}