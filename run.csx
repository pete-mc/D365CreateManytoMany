using System.Net;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
       
    string crmOrg = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "crmorg", true) == 0).Value;
    string apiKey = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "apikey", true) == 0).Value;
    string action = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "action", true) == 0).Value;
    string parentType = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "parentType", true) == 0).Value;
    string parentId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "parentId", true) == 0).Value;
    string childType = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "childType", true) == 0).Value;
    string childId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "childId", true) == 0).Value;
    string relationship = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "relationship", true) == 0).Value;
    if (apiKey != "kjbsdjb4598dvkbk234bSDFsdfdsfbk213213fdsfsdfxvcsfdsf")
    {
        var resBad = new HttpResponseMessage(HttpStatusCode.Forbidden);
        return resBad;
    }

    IServiceManagement<IOrganizationService> orgServiceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri("https://" + crmOrg + ".api.crm6.dynamics.com/XRMServices/2011/Organization.svc"));
    AuthenticationCredentials authCredentials = new AuthenticationCredentials();
    authCredentials.ClientCredentials.UserName.UserName = "PUT USERNAME HERE";
    authCredentials.ClientCredentials.UserName.Password = "PUT PASSWORD HERE";
    AuthenticationCredentials tokenCredentials = orgServiceManagement.Authenticate(authCredentials);
    OrganizationServiceProxy organizationProxy = new OrganizationServiceProxy(orgServiceManagement, tokenCredentials.SecurityTokenResponse);

    switch (action){
        case "associate": 
            log.Info("Associate Request");
            AssociateRequest recordToAssociate = new AssociateRequest
            {
                Target = new EntityReference(parentType, new Guid(parentId)),
                RelatedEntities = new EntityReferenceCollection
                {
                    new EntityReference(childType, new Guid(childId))
                },
                Relationship = new Relationship(relationship)
            };
            organizationProxy.Execute(recordToAssociate);
            break;
        case "disassociate": 
            DisassociateRequest recordToDisassociate = new DisassociateRequest
            {
                Target = new EntityReference(parentType, new Guid(parentId)),
                RelatedEntities = new EntityReferenceCollection
                {
                    new EntityReference(childType, new Guid(childId))
                },
                Relationship = new Relationship(relationship)
            };
            organizationProxy.Execute(recordToDisassociate);
            break;
    }
    var res = new HttpResponseMessage(HttpStatusCode.OK);
    return res;
}
