namespace HMS.Shared.ApplicationService.Hotel.Room
{
    public interface IInformationRoomService
    {
        bool CheckRoom(int roomId);
        int FindHotelRoom(int roomId);
    }
}
