using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrpcServer_PI_21_01.Data
{
    internal class OrgRepository
    {
        // это удалить после привязки БД
        private readonly static List<Organization> OrganizationsMas = new()
        {
            new Organization(1,"МКУ «ЛесПаркХоз»", "3664069397", "770201001", "г. Сургут", "Коммерческий", "действующее"),
            new Organization(2,"ГосОтлов", "9574637594","770495001", "г. Тюмень", "Государственная организация", "действующее"),
            new Organization(3,"ПРОО «Общество защиты животных»", "5769384756", "720294631", "г. Тюмень", "Коммерческий", "действующее")
        };

        public static bool UpdateOrganization(Organization org)
        {
            // на вход получаем новую организацию, нам нужно найти в БД организацию с
            // ID = org.idOrg и апдейтнуть его по всем остальным полям
            var IdOrg = OrganizationsMas.FindIndex(x => x.idOrg == org.idOrg);
            
            OrganizationsMas[IdOrg] = org;

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, контракта с таким Id не существует в БД)
            return true;
        }

        public static bool AddOrganization(Organization org)
        {
            // 'org' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            OrganizationsMas.Add(org);

            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
            return true;
        }

        public static bool RemoveOrganization(int id)
        {
            // ~~old code~~ OrganizationsMas.Remove(organization);

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, организации с таким Id не существует в БД)
            return true;
        }

        public static List<Organization> GetOrganizations()
        {
            // должно забирать все организации из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return OrganizationsMas;
        }
    }
}
