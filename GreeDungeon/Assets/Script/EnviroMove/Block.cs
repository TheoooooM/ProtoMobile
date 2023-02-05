using UnityEngine;

namespace Script.EnviroMove
{
    public class Block : MonoBehaviour, IBoardable
    {
        protected LevelConstructor grid;
        private Vector2 gridPos;

        protected bool haveTop;
        protected bool isTop;

        public void SetMaster(LevelConstructor gridMaster, Vector2 pos)
        {
            grid = gridMaster;
            gridPos = pos;
        }

        public void SetPosition(Vector2 newPos)
        {
            gridPos = newPos;
        }

        public virtual Vector3 GetWorldPosition()
        {
            return transform.position;
        }

        public bool isBlockOnTop() => haveTop;
        public void SetTopState(bool state) => isTop = state;
    }
}
