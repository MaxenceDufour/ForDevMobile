using System;
using BusinessClassPortable;
using DAOClass;
using System.Collections.Generic;

namespace WebServiceForDev
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        private DAORubric _objDaoRubric = DAORubric.getInstance();
        private DAOTopic _objDaoTopic = DAOTopic.getInstance();
        private DAOTopicReply _objDaoTopicReply = DAOTopicReply.getInstance();
        public List<Rubric> GetListRubric()
        {
            return _objDaoRubric.GetListRubric();
        }

        public List<Topic> GetListTopic(string rubricId)
        {
            int rId = Int32.Parse(rubricId);
            return _objDaoTopic.GetLastTopicTop10ById(rId);
        }

        public List<Message> GetListMessage(string topicId)
        {
            int tId = Int32.Parse(topicId);
            var list = _objDaoTopicReply.GetTopicReplyTop10(tId);

            return list;
        }
    }
}
