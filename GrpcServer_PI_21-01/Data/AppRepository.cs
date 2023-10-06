using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GrpcServer_PI_21_01.Data
{
    internal class AppRepository
    {
        // удалить, когда привяжем БД
        private readonly static List<App> Applications = new()
        {
            new App(DateTime.Parse("20-02-2023"), 1, "г. Тюмень", "р-н Ленинский", "около дома 10", "10", "Белая собака с черным ухом, порода неизвестна,", "Физ. лицо"),
            new App(DateTime.Parse("15-02-2023"), 2, "г. Тюмень","р-н Калининский", "около магазина Магнит", "15", "Рыжая собака", "Физ. лицо"),
            new App(DateTime.Parse("20-03-2023"), 3,"г. Сургут", "мкр. 10", "двор дома №6", "7", "Черная собака", "Физ. лицо")
        };

        public static bool UpdateApplication(App app)
        {
            // на вход получаем новую заявку, нам нужно найти в БД заявку с
            // ID = app.number и апдейтнуть его по всем остальным полям
            var id = Applications.FindIndex(x => x.number == app.number);
            Applications[id] = app;

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, контракта с таким Id не существует в БД)
            return true;
        }

        public static bool AddApplication(App app)
        {
            // 'app' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            Applications.Add(app);

            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
            return true;
        }

        public static bool RemoveApplication(int id)
        {
            // ~~old code~~ Applications.Remove(app);
            throw new NotImplementedException();

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, организации с таким Id не существует в БД)
        }

        public static List<string[]> FilterByDate(string filter, string filter2)
        {
            List<App> AppsFilter = Applications.Where(x => x.date >= DateTime.Parse(filter) && x.date <= DateTime.Parse(filter2)).ToList();
            List<string[]> apps = new();
            foreach (App app in AppsFilter)
            {
                var tempApp = new List<string>
                {
                    app.date.ToString(),
                    app.number.ToString(),
                    app.locality,
                    app.territory,
                    app.animalHabiat,
                    app.urgencyOfExecution,
                    app.animaldescription,
                    app.applicantCategory
                };
                apps.Add(tempApp.ToArray());
            }
            return apps;
        }

        public static List<App> GetApplications()
        {
            // должно забирать все заявки из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return Applications;
        }
    }
}
