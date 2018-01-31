using BusinessClassPortable;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebServiceForDev
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        [WebGet(UriTemplate = "Rubrics", ResponseFormat = WebMessageFormat.Json)]
        List<Rubric> GetListRubric();

        [OperationContract]
        [WebGet(UriTemplate = "Topics/{rubricId}", ResponseFormat = WebMessageFormat.Json)]
        List<Topic> GetListTopic(string rubricId);

        [OperationContract]
        [WebGet(UriTemplate = "Messages/{topicId}", ResponseFormat = WebMessageFormat.Json)]
        List<Message> GetListMessage(string topicId);
    }
}