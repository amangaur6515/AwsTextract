using AwsTextract.api.Models;

namespace AwsTextract.api.Services.Abstract
{
    public interface IDocumentValidationService
    {
        public ResponseViewModel ValidateDocument(DocumentViewModel document);
    }
}
