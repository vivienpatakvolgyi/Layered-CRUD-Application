using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Models.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
        // messages
        public ApiResult()
        {

        }

        public ApiResult(bool isSuccess, List<string> messages = null)
        {
            IsSuccess = isSuccess;
            Messages = messages;
        }
    }
}
