using UnityEngine;

namespace Script.EnviroMove
{
    public class Block : MonoBehaviour, IBoardable
    {
        private LevelConstructor grid;
        private Vector2 gridPos;
    
        public void SetMaster(LevelConstructor gridMaster, Vector2 pos)
        {
            grid = gridMaster;
            gridPos = pos;
        }

        public void SetPosition(Vector2 newPos)
        {
            gridPos = newPos;
        }
    }
}
