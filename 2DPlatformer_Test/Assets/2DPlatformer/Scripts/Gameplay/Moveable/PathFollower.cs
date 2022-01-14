namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PathFollower : MonoBehaviour
    {
        [SerializeField]
        private Path _path = null;

        [SerializeField]
        private float _destinationThreshold = 0.5f;

        [SerializeField]
        private float _speed = 1f;

        private int _currentPathIndex = 0;

        private bool _isReverse = false;

        private void Update()
        {
            // Get Next destination ?
            // Check if below threshold ?
            // if so, getnextdistionat
            // if not, Move to 
            Vector3 nextWaypoint = TryGetNextWaypoint();
            MoveToNextWaypoint(nextWaypoint);
        }

        // Recursive function
        private Vector3 TryGetNextWaypoint()
        {
            Vector3 nextWaypoint;
            if (_path.TryGetNextWaypoint(ref _currentPathIndex, out nextWaypoint) == true)
            {
                // If I'm near destination
                if (Vector3.Distance(transform.position, nextWaypoint) < _destinationThreshold)
                {
                    if (_isReverse == false)
                    {
                        _currentPathIndex++;
                    }
                    else
                    {
                        _currentPathIndex--;
                    }

                    if (_path.IsLoop == false)
                    {
                        if (_currentPathIndex >= _path.PathLength - 1)
                        {
                            _isReverse = true;
                        }
                        else if (_currentPathIndex <= 0)
                        {
                            _isReverse = false;
                        }
                    }
                    TryGetNextWaypoint(); // the recursion appears here, we call again our function
                }
            }
            return nextWaypoint;
        }

        private void MoveToNextWaypoint(Vector3 nextWaypoint)
        {
            // Find Direction : direction = targetPosition - selfPosition;
            transform.position += (nextWaypoint - transform.position).normalized * _speed * Time.deltaTime;
        }
    }
}