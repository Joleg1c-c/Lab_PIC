using GrpcClient_PI_21_01.Models;

namespace GrpcClient_PI_21_01.Controllers
{
    internal class LocationService
    {
        public static Location GetLocationFromReply(LocationReply reply)
        {
            return new Location(reply.IdLocation, reply.City);
        }
    }
}
