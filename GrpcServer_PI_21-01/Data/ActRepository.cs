using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer_PI_21_01.Data
{
    class ActRepository
    {
        // удалить это, когда будет прикручена БД
        private readonly static List<Act> acts = new()
        {
            new Act(1, 4, 0, OrgRepository.GetOrganizations()[0], DateTime.Parse("01-06-23"), "Отловить 4 собаки",
                    AppRepository.GetApplications()[0], ContractRepository.GetContracts()[1]),

            new Act(2, 0, 4, OrgRepository.GetOrganizations()[1], DateTime.Parse("02-06-23"), "Отловить 4 кошки",
                    AppRepository.GetApplications()[1], ContractRepository.GetContracts()[0]),

            new Act(3, 3, 2, OrgRepository.GetOrganizations()[2], DateTime.Parse("03-06-23"), "Отловить 3 собаки и 2 кошки",
                    AppRepository.GetApplications()[2], ContractRepository.GetContracts()[1]),

        };

        public static bool UpdateAct(Act actData)
        {
            // на вход получаем новый акт отлова, нам нужно найти в БД акт отлова с
            // ID = actData.ActNumber и апдейтнуть его по всем остальным полям
            var index = acts.FindIndex(x => x.ActNumber == actData.ActNumber);
            acts[index] = actData;

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, акт отлова с таким Id не существует в БД)
            return true;
        }

        public static bool AddAct(Act A)
        {
            // 'A' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            acts.Add(A);

            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
            return true;
        }

        public static bool RemoveAct(int choosedAct)
        {
            var index = acts.FindIndex(x => x.ActNumber == choosedAct);
            acts.RemoveAt(index);

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, акт отлова с таким Id не существует в БД)
            return true;
        }

        public static List<Act> GetActs()
        {
            // должно забирать все акты отлова из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return acts;
        }
    }
}
