﻿using Lab_PIC_5.Data;
using Lab_PIC_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_PIC_5
{
    internal class ActService
    {
        public static List<string[]> ShowAct(string filter)
        {
            List<string[]> acts = stringMassChencher(ActRepository.ShowAct(filter));
            return acts;
        }

        public static void EditAct(string[] takedActData)
        {
            var actData = new Act(int.Parse(takedActData[0]), DateTime.Parse(takedActData[1]), 
                                  OrgRepository.Organizations[OrgRepository.Organizations.FindIndex(x => x.idOrg == takedActData[2])],
                                  takedActData[3],
                                  takedActData[4],
                                  ActRepository.animalCards[ActRepository.animalCards.FindIndex(x => x.IdAnimalCard == int.Parse(takedActData[5]))]);
            ActRepository.SaveActData(actData);
        }

        private AnimalCard FindAnimalCard(int idAnimalCard)
        {
            var index = ActRepository.animalCards.FindIndex(x => x.IdAnimalCard == idAnimalCard);
            return ActRepository.animalCards[index];
        }

        private static List<string[]> stringMassChencher(List<Act> acts)
        {
            List<string[]> result = new List<string[]>();
            foreach (Act act in acts)
            {
                var oldAct = new List<string>
                {
                    act.ActNumber.ToString(),
                    act.Date.ToString(),
                    act.Organization.name,
                    act.Contracts,
                    act.Application,
                    act.AnimalCard.Kind
                };
                result.Add(oldAct.ToArray());
            }
            return result;
        }
    }
}
