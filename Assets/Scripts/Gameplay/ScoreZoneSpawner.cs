using System.Collections.Generic;
using UnityEngine;

namespace Minigames.LivingRoom.Play_Time
{
    public class ScoreZoneSpawner : MonoBehaviour
    {
        private LineRenderer _zoneLineRenderer;
        private EdgeCollider2D _collider2D;

        private float lastAngle = 0;

        private void Awake()
        {
            TryGetComponent(out _zoneLineRenderer);
            TryGetComponent(out _collider2D);
        }

        private Vector3 SetNewZoneStarter(float radius)
        {
            float angle = Mathf.Floor(Random.Range(0, 2*Mathf.PI));

            if (angle == lastAngle)
            {
                return SetNewZoneStarter(radius);
            } 
            
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            float z = 0;

            lastAngle = angle;

            return new Vector3(x, y, z);
        }

        public void CreateNewZone(float radius, float distanceBetweenPoints, float steps)
        {
            var positions = new List<Vector2>(); 
            
            _zoneLineRenderer.positionCount = Mathf.FloorToInt(steps);

            Vector3 startPos = SetNewZoneStarter(radius);
            
            _zoneLineRenderer.SetPosition(0,startPos);
            positions.Add(startPos);

            for (int i = 1; i < _zoneLineRenderer.positionCount; i++)
            {
                float newAngle = CalculateRads(_zoneLineRenderer.GetPosition(i - 1), distanceBetweenPoints);

                float x = radius * Mathf.Cos(newAngle);
                float y = radius * Mathf.Sin(newAngle);
                float z = 0;

                Vector3 newPos = new Vector3(x, y, z);
            
                _zoneLineRenderer.SetPosition(i, newPos);
                positions.Add(newPos);
            }

            _collider2D.SetPoints(positions);
        }

        private float CalculateRads(Vector3 lastPos, float distanceBetweenPoints)
        {
            Vector3 direction = lastPos - transform.position;
            return Mathf.Atan2(direction.y, direction.x) + distanceBetweenPoints;
        }

        public Vector3 GetMiddleOfZone(float steps)
        {
            return _zoneLineRenderer.GetPosition(Mathf.FloorToInt(steps)/2);
        }
    }
}
