using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeScanner.Services
{
    public interface IRestClient
    {
        Task<string> GetRequisites(string address);
        Task<string> GetDocumentData(string address);
        Task<string> PutXmlSign(string address, string json, string xmlSign);
    }
}
