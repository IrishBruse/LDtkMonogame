using LDtk;

namespace Examples
{
    public class Door : Entity
    {
        // LDtk entity fields
        public string destinationLevel;
        public int destinationDoor;

        public bool opening;
        public Rect trigger;

        public void Update(float deltaTime)
        {

        }
    }
}