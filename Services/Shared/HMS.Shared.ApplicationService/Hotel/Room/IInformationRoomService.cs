﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Shared.ApplicationService.Hotel.Room
{
    public interface IInformationRoomService
    {
        bool CheckRoom(int roomId);
        int FindHotelRoom(int roomId);
    }
}
