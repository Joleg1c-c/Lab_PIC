using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer_PI_21_01.Data
{
    class LocationRepository
    {
        // это удалить когда БД будет привязана
        private readonly static List<Location> locationCosts = new()
        { new Location(1, "г. Тюмень"), 
            new Location(2, "г. Тобольск"),
            new Location(3, "г. Сургут")};

        public static List<Location> GetLocations()
        {
            // должно забирать все местности из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return locationCosts;
        }

        public static bool AddLocation(Location loc)
        {
            // 'loc' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            throw new NotImplementedException();

            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
        }

        public static bool RemoveLocation(int id)
        {
            throw new NotImplementedException();

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, местности с таким Id не существует в БД)
        }

        public static bool UpdateLocation(Location loc)
        {
            // на вход получаем новую местность, нам нужно найти в БД местность с
            // ID = loc.IdLocation и апдейтнуть его по всем остальным полям
            throw new NotImplementedException();

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, местности с таким Id не существует в БД)
        }
    }
}
