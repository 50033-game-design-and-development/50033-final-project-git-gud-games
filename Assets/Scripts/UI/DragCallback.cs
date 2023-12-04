using UnityEngine.UIElements;

public interface DragCallbacks {
    void OnDragStart(IPointerEvent evt);
    void OnHoverStart(MouseEnterEvent evt);
    void OnHoverEnd(MouseLeaveEvent evt);
    void OnDragEnd(IPointerEvent evt);
}
