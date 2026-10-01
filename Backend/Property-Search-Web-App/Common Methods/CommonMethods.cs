namespace Property_Search_Web_App.Common_Methods
{
    public class CommonMethods
    {
        // Success Messages
        public string GetAllPropertiesSuccess => "Properties retrieved successfully.";
        public string GetPropertySuccess => "Property retrieved successfully.";
        public string GetAllSpacesSuccess => "Spaces retrieved successfully.";
        public string PropertyCreatedSuccess => "Property created successfully.";

        //Error Messages
        public string GetAllPropertiesFailed => "Failed to retrieve properties. Please try again.";
        public string GetPropertyNotFound => "Property not found with the given ID.";
        public string SpacesNotFound => "No spaces found matching the specified criteria.";
        public string InternalServerError => "An internal server error occurred. Please try again later.";
        public string ModelStateNotValid => "The provided data is not valid. Please check and try again.";

        
        public string PropertyIdRequired => "Property ID is required for this action.";

        
        public string RequestFailed => "The request failed. Please try again later.";
    }
}
