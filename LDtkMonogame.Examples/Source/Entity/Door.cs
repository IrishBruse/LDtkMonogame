using LDtk;

namespace Examples
{
    public class Door : Entity
    {
        // LDtk entity fields
        public string destinationLevel;
        public int destinationDoor;


        public Rect trigger;
        public bool opening;

        public void Update(float deltaTime)
        {

        }
    }
}