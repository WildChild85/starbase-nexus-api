namespace starbase_nexus_api.Models.Api
{
    public class PatchOperation
    {
        public const string OPERATION_REPLACE = "replace";

        public string Op { get; set; }

        public string Path { get; set; }

        public object Value { get; set; }
    }
}
