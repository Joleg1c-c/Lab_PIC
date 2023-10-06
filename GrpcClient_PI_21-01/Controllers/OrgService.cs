﻿//using GrpcClient_PI_21_01.Data;
using GrpcClient_PI_21_01.Models;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClient_PI_21_01.Controllers
{
    internal class OrgService
    {
        //public static List<string[]> ShowOrganizations()
        //{
        //    List<string[]> orgs = new List<string[]>();
        //    foreach (Organization org in OrgRepository.Organizations)
        //    {
        //        var tempOrg = new List<string>
        //        {
        //            org.idOrg.ToString(),
        //            org.name,
        //            org.INN,
        //            org.KPP,
        //            org.registrationAdress,
        //            org.type,
        //            org.status
        //        };
        //        orgs.Add(tempOrg.ToArray());
        //    }
        //    return orgs;
        //}

        //public static void AddOrganization(Organization Org)
        //{
        //    OrgRepository.SaveAdd(Org);
        //}

        //public static void EditOrganization(Organization Org)
        //{
        //    OrgRepository.Save(Org);
        //}

        //public static void DeleteOrganization(string org)
        //{
        //    foreach (Organization organization in OrgRepository.Organizations)
        //    {
        //        if (organization.INN == Convert.ToString(org))
        //        {
        //            OrgRepository.Del(organization);
        //            break;
        //        }

        //    }
        //}

        public static Organization GetOrganizationFromReply(OrganizationReply reply)
        {
            return new Organization(reply.IdOrganization,
                reply.Name,
                reply.INN,
                reply.KPP,
                reply.RegistrationAddress,
                reply.Type,
                reply.Status);
        }

        public static string[] ToDataArray(Organization org)
        {
            return new string[] {
                    org.idOrg.ToString(),
                    org.name,
                    org.INN,
                    org.KPP,
                    org.registrationAdress,
                    org.type,
                    org.status
            };
        }

        public static async Task<List<Organization>> GetOrganizations()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7275");
            var client = new DataRetriever.DataRetrieverClient(channel);
            var serverData = client.GetOrganizations(new Google.Protobuf.WellKnownTypes.Empty());
            var responseStream = serverData.ResponseStream;
            var orgs = new List<Organization>();
            await foreach (var response in responseStream.ReadAllAsync())
            {
                orgs.Add(GetOrganizationFromReply(response));
            }
            return orgs;
        }
    }
}