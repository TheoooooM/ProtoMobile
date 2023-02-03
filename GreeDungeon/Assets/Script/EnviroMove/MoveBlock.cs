using UnityEngine;

namespace Script.EnviroMove
{
    public class MoveBlock : Block, IInteractable, IBoardable
    {
        private Vector2 gridPosition; 
        private Vector3 startScale;

        private void Start()
        {
            startScale = transform.localScale;
        }


        public void Select()
        {
            Debug.Log("Receive Touch");
            transform.localScale = startScale * 1.2f;
        }

        public void Deselect()
        {
            transform.localScale = startScale;
        }

        public void Swipe(Enums.Side side)
        {
            if(grid.TryMove(gridPosition, side, this))
            {
                switch (side)
                {
                    case Enums.Side.up: transform.position+= Vector3.forward; break;
                    case Enums.Side.left: transform.position+= Vector3.left; break;
                    case Enums.Side.right: transform.position+= Vector3.right; break;
                    case Enums.Side.down: transform.position+= Vector3.back; break;
                }   
            }
        }


        public new void SetMaster(LevelConstructor gridMaster, Vector2 pos)
        {
            grid = gridMaster;
            gridPosition = pos;
        }

        public new void SetPosition(Vector2 newPos)
        {
            gridPosition = newPos;
        }
    }
}
