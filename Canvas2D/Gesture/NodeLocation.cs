using OpenTK;

namespace Canvas2D.Gesture {
    public abstract class NodeLocation {
        private Vector3 location;

        public Vector3 Location { get => location; set => location = value; }

        abstract public void Draw();

        abstract public void Draw2D();
    }
}
