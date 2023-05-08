
namespace CreativeUrge.Camera
{
    public interface ICameraController
    {
        bool InputEnabled { set; }

        void EnableInput()
        {
            InputEnabled = true;
        }

        void DisableInput()
        {
            InputEnabled = false;
        }
    }
}
