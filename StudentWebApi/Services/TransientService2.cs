namespace StudentWebApi.Services
{
    public class TransientService
    {
        public int Counter { get; set; }
        public TransientService(ScopedService scopedService)
        {
            scopedService.Counter++;
        }
    }
}
