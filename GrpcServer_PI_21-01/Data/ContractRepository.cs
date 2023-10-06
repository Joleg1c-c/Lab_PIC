using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer_PI_21_01.Data
{
    class ContractRepository
    {
        // это удалить когда БД будет привязана
        private readonly static List<Contract> contract = new()
        { 
            new Contract(1, DateTime.Parse("02.05.2023"), DateTime.Parse("05.05.2023"), 
                LocationRepository.GetLocations()[0], 1000, 
                OrgRepository.GetOrganizations()[0], OrgRepository.GetOrganizations()[1]),

            new Contract(2, DateTime.Parse("12.05.2023"), DateTime.Parse("15.05.2023"), 
                LocationRepository.GetLocations()[1], 2000, 
                OrgRepository.GetOrganizations()[1], OrgRepository.GetOrganizations()[1]),

            new Contract(3, DateTime.Parse("10.05.2023"), DateTime.Parse("19.05.2023"),
                LocationRepository.GetLocations()[2], 1550,
                OrgRepository.GetOrganizations()[2], OrgRepository.GetOrganizations()[1]),

            new Contract(4, DateTime.Parse("20.05.2023"), DateTime.Parse("25.05.2023"),
                LocationRepository.GetLocations()[0], 2100,
                OrgRepository.GetOrganizations()[0], OrgRepository.GetOrganizations()[2])
        };

        public static bool UpdateContract(Contract cont)
        {
            // на вход получаем новый контракт, нам нужно найти в БД контракт с
            // ID = cont.IdContract и апдейтнуть его по всем остальным полям
            var index = contract.FindIndex(x => x.IdContract == cont.IdContract); // старый код, на замену
            contract[index] = cont; // старый код, на замену

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, контракта с таким Id не существует в БД)
            return true;
        }

        public static bool AddContract(Contract cont)
        {
            // 'cont' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            contract.Add(cont); // старый код, на замену
            
            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
            return true;
        }

        public static bool DeleteContract(int id)
        {
            // contract.Remove(cont); // старый код, на замену

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, контракта с таким Id не существует в БД)
            return true;
        }

        public static List<Contract> GetContracts()
        {
            // должно забирать все контракты из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return contract;
        }
    }
}
