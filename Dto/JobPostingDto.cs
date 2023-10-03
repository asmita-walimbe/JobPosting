namespace JobPosting.Dto
{
    public record JobPostingDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
