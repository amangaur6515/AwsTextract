using AwsTextract.api.Models;
using AwsTextract.api.Services.Abstract;
using PdfSharp.Pdf.IO;

namespace AwsTextract.api.Services.Implementation
{
    public class DocumentValidationService:IDocumentValidationService
    {
        public ResponseViewModel ValidateDocument(DocumentViewModel document)
        {
            if (document.Document == null || document.Document.Length == 0)
            {
                return new ResponseViewModel
                {
                    Message = "No document provided",
                    IsValid = false
                };

            }
            if(document.Document.ContentType!="application/pdf" &&  document.Document.ContentType!="image/png" && document.Document.ContentType != "image/jpeg")
            {
                return new ResponseViewModel
                {
                    Message = "Invalid Document type. Only PDF and images are allowed",
                    IsValid = false

                };
            }
            //check content type
            if (document.Document.ContentType == "application/pdf")
            {
                // PDF handling logic with PdfSharp
                using (var pdfStream = document.Document.OpenReadStream())
                {
                    try
                    {
                        // Load PDF document
                        var pdfDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import);

                        // Check if the PDF has more than 2 pages
                        if (pdfDocument.PageCount > 2)
                        {
                            return new ResponseViewModel
                            {
                                Message = "Pdf having more than 2 pages is not allowed",
                                IsValid = false
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ResponseViewModel
                        {
                            Message = $"Error processing PDF: {ex.Message}",
                            IsValid= false
                        };
                    }
                }
            }
            else if (document.Document.ContentType != "image/jpeg" && document.Document.ContentType != "image/png")
            {
                return new ResponseViewModel
                {
                    Message = "Only JPEG and PNG is allowed",
                    IsValid = false
                };

            }

            return new ResponseViewModel
            {
                Message = "Document is valid",
                IsValid = true
            };
        }
    }
}
