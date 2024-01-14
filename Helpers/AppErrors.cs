using QlhsServer.Models.Response;

namespace QlhsServer.Helpers
{
    public class AppErrors
    {

        public static ErrorModel LoginEmailOrPasswordIncorrect = new ErrorModel
        {
            Status = StatusCodes.Status202Accepted,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "General",
                    Message = "Email or password is incorrect",
                    Code = "000005"
                }
            }
        };

        public static ErrorModel EmailIsAlreadyTakenError = new ErrorModel
        {
            Status = StatusCodes.Status202Accepted,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "Email",
                    Message = "Email is already taken",
                    Code = "000006"
                }
            }
        };

        public static ErrorModel FileExtensionIsNotSupportedError = new ErrorModel
        {
            Status = StatusCodes.Status415UnsupportedMediaType,
            Errors = new List<ErrorModel.ErrorItem>
            {
                new ErrorModel.ErrorItem
                    {
                        FieldName = "File",
                        Message = "File type is not supported",
                        Code = "000415"
                    }
            }
        };

        public static ErrorModel FileNotFound = new ErrorModel
        {
            Status = StatusCodes.Status404NotFound,
            Errors = new List<ErrorModel.ErrorItem>
            {
                new ErrorModel.ErrorItem
                    {
                        FieldName = "File",
                        Message = "File not found",
                        Code = "000404"
                    }
            }
        };

        public static ErrorModel AuthenticatedError = new ErrorModel
        {
            Status = StatusCodes.Status401Unauthorized,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "Authenticated",
                    Message = "You are not authenticated",
                    Code = "000401"
                }
            }
        };

        public static ErrorModel AuthorizedError = new ErrorModel
        {
            Status = StatusCodes.Status403Forbidden,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "Authorized",
                    Message = "You are not authorized",
                    Code = "000403"
                }
            }
        };


        public static ErrorModel InternalServerError = new ErrorModel
        {
            Status = StatusCodes.Status500InternalServerError,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "General",
                    Message = "Internal server error",
                    Code = "000500"
                }
            }
        };



        public static ErrorModel UserNotFoundError = new ErrorModel
        {
            Status = StatusCodes.Status404NotFound,
            Errors = new List<ErrorModel.ErrorItem> {
                new ErrorModel.ErrorItem {
                    FieldName = "User",
                    Message = "User not found",
                    Code = "000404"
                }
            }
        };




    };
}