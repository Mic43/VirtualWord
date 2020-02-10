namespace VirtualWord.Behaviours.RotateBehaviour
{
    public class DoNotRotate : IRotationBehaviour
    {
        public double GetNewRotationAngle(double oldDirectionAngle)
        {
            return oldDirectionAngle;
        }
    }
}