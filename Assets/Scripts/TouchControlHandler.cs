using UnityEngine;

namespace DefaultNamespace
{
    public static class TouchControlHandler
    {
        public static Collider2D getOnTouchCollider(Touch touch)
        {
            Vector3 wp= Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            return Physics2D.OverlapPoint(touchPos);
        }

        public static Collider2D GetOnVector2Collider(Vector2 position)
        {
            return Physics2D.OverlapPoint(position);
        }
    }
}