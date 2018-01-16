using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SGEGService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "SGEGService" à la fois dans le code et le fichier de configuration.
    public class SGEGService : ISGEGPublicService, ISGEGPrivateService
    {
        public string GetPublicMessage()
        {
            return "Public Message : Hello world!";
        }

        public string GetPrivateMessage()
        {
            return "Private Message : YOLO !!";
        }
    }
}
