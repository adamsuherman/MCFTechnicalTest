namespace MCFTechnicalTestFrontEnd.Models
{
    public class SaveResult
    {
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }

    public class SaveRequest
    {
        public string agreement_number { get; set; } = null!;
        public string? bpkb_no { get; set; }
        public string? branch_id { get; set; }
        public DateTime? bpkb_date { get; set; }
        public string? faktur_no { get; set; }
        public DateTime? faktur_date { get; set; }
        public string? location_id { get; set; }
        public string? policy_no { get; set; }
        public DateTime? bpkb_date_in { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
    }
}
