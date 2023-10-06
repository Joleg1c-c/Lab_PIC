using GrpcServer_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer_PI_21_01.Data
{
    class AnimalRepository
    {
        // удалить после привязки БД
        private readonly static List<AnimalCard> animalCards = new()
        {
            new AnimalCard(1, "Собака", "Женский", "Лабрадор", 40, "Длинная", "Рыжий",
                            "Висячие", "Длинный", "ID101", "Метка 2",
                            LocationRepository.GetLocations()[0],
                            ActRepository.GetActs()[0],
                            null),

            new AnimalCard(2, "Кот", "Мужской", "Британская", 30, "Короткая", "Черный",
                            "Прямые", "Короткий", "ID302", "Метка 1",
                            LocationRepository.GetLocations()[0],
                            ActRepository.GetActs()[1],
                            null),

            new AnimalCard(4, "Собака", "Женский", "Немецкая овчарка", 50, "Длинная", "Черно-серый",
                            "Висячие", "Длинный", "ID041", "Метка 4",
                            LocationRepository.GetLocations()[0],
                            ActRepository.GetActs()[2],
                            null),

            new AnimalCard(3, "Кот", "Мужской", "Сиамская", 25, "Короткая", "Серый",
                            "Прямые", "Длинный", "ID201", "Метка 3",
                            LocationRepository.GetLocations()[0],
                            ActRepository.GetActs()[2],
                            null),
        };

        public static bool AddAnimalCard(AnimalCard animalCard)
        {
            // 'animalCard' подаётся с Id = -1. После добавления в БД нужно присвоить
            // этому ссылочному значению новое Id, которое было присвоено самой БД
            animalCards.Add(animalCard);

            // возвращаем true, если добавление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, поле, которое не может быть null, вдруг стало null)
            return true;
        }

        public static bool UpdateAnimalCard(AnimalCard animalCard)
        {
            // на вход получаем новую карточку животного, нам нужно найти в БД карточку животного с
            // ID = animalCard.IdAnimalCard и апдейтнуть его по всем остальным полям
            throw new NotImplementedException();

            // возвращаем true, если обновление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, карточки животного с таким Id не существует в БД)
        }

        public static bool RemoveAnimalCard(int id)
        {
            throw new NotImplementedException();

            // возвращаем true, если удаление произошло успешно,
            // вовзращаем false, если что-то пошло не так (например, карточки животного с таким Id не существует в БД)
        }

        public static List<AnimalCard> GetAnimalCards()
        {
            // должно забирать все карточки животного из БД (желательно сделать кэширование:
            // один раз читается и результат сохраняется на, например, 5 секунд, т.е. любой вызов
            // этого метода в течение 5 секунд возвращает кэшированное значение)
            // P.S. кэширование должно очищаться после выполнения других действий CRUD кроме Read
            return animalCards;
        }
    }
}
