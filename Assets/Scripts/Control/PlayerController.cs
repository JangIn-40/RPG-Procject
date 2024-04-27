using UnityEngine;
using RPG.Movement;
using System;
using RPG.Attributes;
using UnityEngine.EventSystems;
using UnityEngine.AI;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hospot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 0f;
        [SerializeField] float maxNavPathLength = 40f;

        Health health;
        void Awake()
        {
            health = GetComponent<Health>();
        }
        
        void Update()
        {
            if(InteractWithUI()) { return; }
            if(health.IsDie()) 
            {
                SetCursor(CursorType.None);
                return;
            }
            
            if(InteractWithComponent()) { return; }
            if(InteractWithMovement()) { return; }

            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hospot, CursorMode.Auto);
        }

        CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach(RaycastHit hit in hits)
            {
                IRayCastable[] rayCastables = hit.transform.GetComponents<IRayCastable>();
                foreach (IRayCastable rayCastable in rayCastables)
                {
                    if(rayCastable.HandleRaycast(this))
                    {
                        SetCursor(rayCastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }        

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            float[] distances = new float[hits.Length];
            for(int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);


            return hits;
        }

        private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNaveMesh(out target);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveToAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        bool RaycastNaveMesh(out Vector3 target)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            target = new Vector3();

            NavMeshHit navMeshHit;
            if(!hasHit) return false;
            
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if(!hasCastToNavMesh) return false;
            
            target = navMeshHit.position;
            
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if(!hasPath) return false;
            if(path.status != NavMeshPathStatus.PathComplete) return false;
            if(GetPathLength(path) > maxNavPathLength) return false;
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0f;
            if(path.corners.Length < 2) return total;
            for(int i = 0; i < path.corners.Length - 1; ++i)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            
            return total;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
