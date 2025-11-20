public enum VehicleDirections { Forward = 1, Backward = 2, Left = 3, Right = 4, Stop = 0 }

public static class VehicleDirectionUtility
{
    public static VehicleDirections GetRandomDirection(VehicleDirections exclude)
    {
        VehicleDirections[] all =
        {
            VehicleDirections.Forward,
            VehicleDirections.Backward,
            VehicleDirections.Left,
            VehicleDirections.Right
        };

        VehicleDirections result;
        do
        {
            result = all[UnityEngine.Random.Range(0, all.Length)];
        }
        while (result == exclude);

        return result;
    }
}
