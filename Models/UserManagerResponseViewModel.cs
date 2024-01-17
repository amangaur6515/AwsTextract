using System.Diagnostics.Eventing.Reader;

namespace AwsTextract.api.Models
{
    public class UserManagerResponseViewModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<String> Errors { get; set; }
        
    }
}
